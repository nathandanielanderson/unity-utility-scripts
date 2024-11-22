using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    public GameObject playerCamera; // Assign the ThirdPersonCamera GameObject here instead of just MainCamera
    public MonoBehaviour[] localOnlyScripts; // Array for local-only scripts

    void Start()
    {
        if (isLocalPlayer)
        {
            // Enable the camera and set MainCamera tag for the local player
            playerCamera.SetActive(true);
            Camera mainCam = playerCamera.GetComponentInChildren<Camera>();
            if (mainCam != null) mainCam.tag = "MainCamera";

            // Enable all local-only scripts for the local player
            foreach (var script in localOnlyScripts)
            {
                script.enabled = true;
            }
        }
        else
        {
            // Disable the entire ThirdPersonCamera GameObject for remote players
            playerCamera.SetActive(false);
        }
    }

    /// <summary>
    /// Handles item interaction logic when the local player clicks an item.
    /// </summary>
    public void TryPickupItem(GameObject item)
    {
        if (!isLocalPlayer) return;

        // Check if the item has the appropriate tag or component
        if (item.CompareTag("Pickup") && item.GetComponent<NetworkIdentity>())
        {
            // Send a command to the server to handle item pickup
            CmdPickupItem(item);
        }
    }

    /// <summary>
    /// Command to handle item pickup on the server.
    /// </summary>
    [Command]
    private void CmdPickupItem(GameObject item)
    {
        if (item == null) return;

        // Validate the item on the server
        if (item.CompareTag("Pickup"))
        {
            // Perform server-side logic for the item (e.g., destroy or transfer ownership)
            Debug.Log($"Server: Player {connectionToClient.connectionId} picked up {item.name}");

            // Destroy the item across the network
            NetworkServer.Destroy(item);
        }
    }
}