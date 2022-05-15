using UnityEngine;
using Utilities;

namespace CameraFollow
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform target;
        
        [SerializeField] private float distance = 10.0f;
        [SerializeField] private float height = 5.0f;
        [SerializeField] private float rotationXOffset = 10f;

        [SerializeField] private float rotationDamping;
        [SerializeField] private float heightDamping;
        
        void LateUpdate()
        {
            if (!target)
                return;
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle =
                Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Always look at the target
            transform.LookAt(target);
            
        }
    }
}