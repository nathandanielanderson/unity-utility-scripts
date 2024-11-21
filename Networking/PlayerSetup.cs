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
}