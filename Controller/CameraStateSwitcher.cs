using UnityEngine;
using Invector.vCamera;

public class CameraStateSwitcher : MonoBehaviour
{
    private vThirdPersonCamera thirdPersonCamera;
    private bool isFreeCamActive = false;
    
    void Start()
    {
        // Get the vThirdPersonCamera component
        thirdPersonCamera = FindObjectOfType<vThirdPersonCamera>();

        // Start with the Follow Cam state
        if (thirdPersonCamera != null)
            thirdPersonCamera.ChangeState("FollowCam");
    }

    void Update()
    {
        // Check for left mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            isFreeCamActive = true;
            thirdPersonCamera.ChangeState("FreeDirectionalCam");
        }
        
        // Check for left mouse button release
        if (Input.GetMouseButtonUp(0))
        {
            isFreeCamActive = false;
            thirdPersonCamera.ChangeState("FollowCam");
        }
    }
}