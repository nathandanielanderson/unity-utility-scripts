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

        if (isServer)  // If this is the server
        {
            HandleServerAction(obj);  // Call server-specific action handling
        }
        else if (isClient)  // If this is a client
        {
            CmdTriggerAction(obj);  // Send a command to the server to handle the action
        }
    }

    [Command]  // Server command
    private void CmdTriggerAction(GameObject obj)
    {
        HandleServerAction(obj);  // Call the server-side logic
    }

    private void HandleServerAction(GameObject obj)
    {
        if (obj != null)
        {
            // Trigger actions on the object
            RpcTriggerAction(obj);

            // Destroy the object on the server (and sync destruction across all clients)
            if (baseTriggerAction.destroyAfter)
            {
                NetworkServer.Destroy(obj);
            }
        }
    }

    [ClientRpc]  // Executes on all clients
    private void RpcTriggerAction(GameObject obj)
    {
        if (obj != null)
        {
            // Invoke Unity events and trigger actions
            baseTriggerAction.OnPressActionInput.Invoke();
            baseTriggerAction.onPressActionInputWithTarget.Invoke(obj);

            // Optionally destroy the object locally (if delay is needed before server syncs destruction)
            if (!isServer && baseTriggerAction.destroyAfter)
            {
                Destroy(obj, baseTriggerAction.destroyDelay);  // Local destruction
            }
        }
    }
}