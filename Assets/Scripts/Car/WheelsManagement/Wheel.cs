using System;
using UnityEngine;
using Utilities;

namespace Car.WheelsManagement
{
    public class Wheel : MonoBehaviour
    {
        public float WheelRPM => collider.rpm;

        [SerializeField] private WheelCollider collider;
        
        //temporary place for const values - will be moved to scriptable object
        private const float RotationSpeedMultiplier = 0.5f;
        private float _initialAngle;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _initialAngle = collider.steerAngle;
        }

        /// <summary>
        /// Applies toprque on collider
        /// </summary>
        /// <param name="force">
        /// Torque for the wheel
        /// </param>
        public void ApplyMotorTorque(float force)
        {
            collider.motorTorque = force;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        public void ApplyBrakeTorque(float force)
        {
            collider.brakeTorque = force;
        }
        
        /// <summary>
        /// Update wheel postion
        /// </summary>
        public void UpdateWheel()
        {
            Vector3 pos;
            Quaternion rot;
            
            collider.GetWorldPose(out pos, out rot);

            _transform.rotation = rot;
            _transform.position = pos;
        }

        /// <summary>
        /// Auto centers wheel
        /// </summary>
        private void HandleAutoCenteringWheels()
        {
            collider.steerAngle += -Mathf.Sign(collider.steerAngle) * RotationSpeedMultiplier/2;

            if (Mathf.Abs(collider.steerAngle - _initialAngle) <= (RotationSpeedMultiplier +1))
            {
                collider.steerAngle = _initialAngle;
            }
        }

        /// <summary>
        /// Steers the wheel
        /// </summary>
        /// <param name="angle">
        /// Angle to steer with
        /// </param>
        public void SetSteeringAngle(float angle)
        {
            collider.steerAngle = angle;
        }
    }
}