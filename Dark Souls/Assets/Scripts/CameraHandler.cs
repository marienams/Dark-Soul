using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//namespace fourth { 
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;
        private Vector3 cameraFollowVeocity = Vector3.zero; // for smooth damp

        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSPeed = 0.03f;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        private float cameraSphereRadius;
        private float cameraCollisionOffset = 0.2f;
        private float minCameraCollissionOffset = 0.2f;
        


        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVeocity, delta / followSpeed);
            //smoothdamp is a lot smoother than lerp for camera movement
            //Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed); // for camera movement around the player 
            myTransform.position = targetPosition;
            CameraCollisionHandler(delta);
        }

        public void HandleRotation(float delta, float mouseXinput, float mouseYinput)
        {
            lookAngle += (mouseXinput * lookSpeed) / delta;
            pivotAngle -= (mouseYinput * pivotSPeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        public void CameraCollisionHandler (float delta)
        {
            targetPosition = defaultPosition;
            RaycastHit hit; // structure used to get back info from raycast
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize(); // to smoothen out any glitch
            //shpere cast function
            if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetPosition = -(dis - cameraCollisionOffset);

            }
             if(Mathf.Abs(targetPosition) < minCameraCollissionOffset)
            {
                targetPosition = -minCameraCollissionOffset;

            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;


            }

        

    }
    


//}
