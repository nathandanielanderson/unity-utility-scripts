using UnityEngine;
using Invector.vCharacterController;

namespace Invector.vCharacterController
{
    public class vThirdPersonMouseInput : vThirdPersonInput
    {
        private bool isLeftMouseButtonPressed = false;

        protected override void Update()
        {
            // Check if the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                isLeftMouseButtonPressed = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isLeftMouseButtonPressed = false;
            }

            // Enable or disable camera rotation inputs based on the left mouse button state
            if (isLeftMouseButtonPressed)
            {
                rotateCameraXInput = new GenericInput("Mouse X", "RightAnalogHorizontal", "Mouse X");
                rotateCameraYInput = new GenericInput("Mouse Y", "RightAnalogVertical", "Mouse Y");
            }
            else
            {
                rotateCameraXInput = new GenericInput("", "", "");
                rotateCameraYInput = new GenericInput("", "", "");
            }

            base.Update();
        }
    }
}