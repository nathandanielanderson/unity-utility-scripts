using UnityEngine;
using Mirror;

public class Coin : NetworkBehaviour
{
    private bool isPlayerInRange = false; // Tracks if the player is in the trigger range

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player and is the local player
        if (other.CompareTag("Player") && other.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player and is the local player
        if (other.CompareTag("Player") && other.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        // Check if the player is in range and right-clicks
        if (isPlayerInRange && Input.GetMouseButtonDown(1)) // 1 is the right mouse button
        {
            CmdRequestDestroy();
        }
    }

    [Command]
    private void CmdRequestDestroy(NetworkConnectionToClient sender = null)
    {
        // Assign authority to the client temporarily
        AssignAuthority(sender);

        // Destroy the coin on the server (and sync to all clients)
        NetworkServer.Destroy(gameObject);
    }

    private void AssignAuthority(NetworkConnectionToClient sender)
    {
        if (sender != null)
        {
            // Assign authority to the requesting client
            GetComponent<NetworkIdentity>().AssignClientAuthority(sender);
        }
    }
}