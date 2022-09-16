using System.Collections;
using System.Collections.Generic;
using First;
using UnityEngine;

namespace Second
{
    public class PlayerLocomotion : MonoBehaviour
    {
        
        Transform cameraObject;
        InputHandler inputHandler; // instance of input Handler class
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public Animationhandler animationhandler;


        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Speed Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;



        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;

            animationhandler = GetComponentInChildren<Animationhandler>();

            animationhandler.Initialize();
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);  //passing time.deltatime in movement function in input handler class

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;  // your basic horizontal and vertical movements, but why the plus in second one
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            moveDirection *= speed; // pretty much same as standard input

            Vector3 projectileVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector); // getting the current vectors projected on the orthogonal plane
            rigidbody.velocity = projectileVelocity; // I dont get this part

            if (animationhandler.canRotate)
            {
                HandleRotation(delta);
            }

            animationhandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float var)
        {
            Vector3 targetDir = Vector3.zero; //setting target vector3 to zero;
            float moveOverride = inputHandler.moveAmount; // collecting move amount from input handler class for spedd I guess

            targetDir = cameraObject.forward * inputHandler.vertical; // assigning camera forward movement based on vertical input from player
            targetDir += cameraObject.right * inputHandler.horizontal; // for rotation?

            targetDir.Normalize(); // make this vector have a magnitude of 1 , why?

            targetDir.y = 0; //no off the ground movement for the player

            if(targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }


            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir); //getting rotation based on currecnt Vector3
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * var); // a quaternion spherically interpolated between current rotation and target rotation

            myTransform.rotation = targetRotation;
        }
        #endregion


    }
}

