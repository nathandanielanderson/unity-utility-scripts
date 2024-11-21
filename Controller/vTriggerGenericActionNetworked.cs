using Mirror;  // Mirror networking library
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Invector.vCharacterController.vActions;

public class vTriggerGenericActionNetworked : NetworkBehaviour
{
    public vTriggerGenericAction baseTriggerAction;  // Reference to the base trigger action

    public IEnumerator ExecutePressActionDelay(GameObject obj)  // New method without override
    {
        yield return new WaitForSeconds(baseTriggerAction.onPressActionDelay);

        if (obj && isServer)  // Check if this is the server
        {
            RpcTriggerAction(obj);  // Call a client RPC to trigger the action across all clients
        }
        else if (obj)  // For clients, send the command to the server without authority check
        {
            CmdTriggerAction(obj);  // Send a command to the server
        }
    }

    [Command]  // Server command
    private void CmdTriggerAction(GameObject obj)
    {
        RpcTriggerAction(obj);  // Call the client RPC from the server
    }

    [ClientRpc]  // Executes on all clients
    private void RpcTriggerAction(GameObject obj)
    {
        if (obj != null)
        {
            baseTriggerAction.OnPressActionInput.Invoke();
            baseTriggerAction.onPressActionInputWithTarget.Invoke(obj);
            if (baseTriggerAction.destroyAfter)
            {
                Destroy(obj, baseTriggerAction.destroyDelay);  // Destroy with delay if needed
            }
        }
    }
}