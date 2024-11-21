using Mirror;  // Mirror networking library
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Invector.vCharacterController.vActions;

public class TriggerPickupNetworked : NetworkBehaviour
{
    public vTriggerGenericAction baseTriggerAction;  // Reference to the base trigger action

    // Handles the delay before performing the action (e.g., pickup delay)
    public IEnumerator ExecutePressActionDelay(GameObject obj)
    {
        yield return new WaitForSeconds(baseTriggerAction.onPressActionDelay);

        if (isServer)  // If this is the server
        {
            HandleServerAction(obj);  // Perform the action directly on the server
        }
        else if (isClient)  // If this is a client
        {
            CmdTriggerPickup(obj);  // Send a command to the server to perform the action
        }
    }

    [Command]  // Command to send from client to server
    private void CmdTriggerPickup(GameObject obj)
    {
        HandleServerAction(obj);  // Perform the action on the server
    }

    private void HandleServerAction(GameObject obj)
    {
        if (obj != null)
        {
            // Invoke the client RPC to sync the action across all clients
            RpcTriggerPickup(obj);

            // Destroy the object on the server (syncs to all clients automatically)
            NetworkServer.Destroy(obj);
        }
    }

    [ClientRpc]  // Client RPC to execute the action on all clients
    private void RpcTriggerPickup(GameObject obj)
    {
        if (obj != null)
        {
            // Invoke Unity events and trigger actions
            baseTriggerAction.OnPressActionInput.Invoke();
            baseTriggerAction.onPressActionInputWithTarget.Invoke(obj);
        }
    }
}