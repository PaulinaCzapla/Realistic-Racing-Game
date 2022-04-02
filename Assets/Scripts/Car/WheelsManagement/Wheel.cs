using System;
using UnityEngine;
using Utilities;

namespace Car.WheelsManagement
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private WheelCollider collider;

        private Transform _transform;
        private Vector3 _initialRotation;
        private float _rotationSpeedMultiplier = 2f;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _initialRotation = _transform.rotation.eulerAngles;
        }

        public void RotateWheel(float direction)
        {
            float angle = Quaternion.Angle(_transform.rotation, Quaternion.Euler(_initialRotation));
            
            if (angle <= 60f || (transform.rotation.z * direction) >= 0)
            {
                _transform.Rotate(new Vector3(0, 0, 1), -direction * _rotationSpeedMultiplier);
            }
        }
    }
}