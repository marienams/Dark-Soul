using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace First  //for organizing the code
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerController inputActions;  // the player controller class we created from input actions option, a class instance
        CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if(cameraHandler != null)
            {
                Debug.Log(cameraInput.x);
                Debug.Log(cameraInput.y);
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleRotation(delta, mouseX, mouseY);
            }
        }

        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerController();
                //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }

        private void MoveInput (float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }

    

    
}


