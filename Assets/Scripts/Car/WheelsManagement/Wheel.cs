using System;
using UnityEngine;
using Utilities;

namespace Car.WheelsManagement
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private WheelCollider collider;

        private Transform _transform;
        private float _rotationSpeedMultiplier = 3f;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void ApplyMotorTorque(float force)
        {
            collider.motorTorque = force;
        }
        
        public void ApplyBrakeTorque(float force)
        {
            collider.brakeTorque = force;
        }
        public void RotateWheel(float direction)
        {
            if (Mathf.Abs(collider.steerAngle) <= 40f || (collider.steerAngle * -direction) >= 0)
            {
                collider.steerAngle += direction * _rotationSpeedMultiplier;
            }
        }

        public void UpdateWheel()
        {
            Vector3 pos;
            Quaternion rot;
            
            collider.GetWorldPose(out pos, out rot);

            _transform.rotation = rot;
            _transform.position = pos;
        }
    }
}