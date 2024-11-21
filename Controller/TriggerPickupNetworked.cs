using Mirror;  // Mirror networking library
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Invector.vCharacterController.vActions;

public class TriggerPickupNetworked : NetworkBehaviour
{
    public vTriggerGenericAction baseTriggerAction;  // Reference to the base trigger action
    private bool hasBeenClaimed = false;  // Prevents multiple players from claiming ownership

    
    // Handles the delay before performing the action (e.g., pickup delay)
    public IEnumerator ExecutePressActionDelay(GameObject obj)
    {
        yield return new WaitForSeconds(baseTriggerAction.onPressActionDelay);

        if (isServer)  // If this is the server
        {
            Debug.Log("This is Server.");
            HandleServerAction(obj);  // Perform the action directly on the server
        }
        else if (isClient)  // If this is a client
        {
            Debug.Log("This is Client.");
            CmdRequestOwnership(obj);  // Request ownership from the server
        }
    }

    [Command]  // Command to request ownership from the server
    private void CmdRequestOwnership(GameObject obj, NetworkConnectionToClient sender = null)
    {
        Debug.Log("CmdRequestOwnership called on server.");
        if (hasBeenClaimed) return; // If already claimed, ignore

        // Assign authority to the requesting player
        obj.GetComponent<NetworkIdentity>().AssignClientAuthority(sender);
        hasBeenClaimed = true;

        // Perform the server-side action
        HandleServerAction(obj);
    }

    private void HandleServerAction(GameObject obj)
    {
        Debug.Log("HandleServerAction called on server.");
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
        Debug.Log("RpcTriggerPickup called on clients.");
        if (obj != null)
        {
            // Invoke Unity events and trigger actions
            baseTriggerAction.OnPressActionInput.Invoke();
            baseTriggerAction.onPressActionInputWithTarget.Invoke(obj);
        }
    }
}