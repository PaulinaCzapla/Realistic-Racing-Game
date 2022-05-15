using UnityEngine;
using Utilities;

namespace CameraFollow
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform target;
        
        [SerializeField] private float distance = 10.0f;
        [SerializeField] private float height = 5.0f;
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
            
            currentRotationAngle =
                Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
            
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
            
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            
            transform.LookAt(target);
        }
    }
}