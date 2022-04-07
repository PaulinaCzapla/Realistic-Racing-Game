using System;
using UnityEngine;

namespace Car.WheelsManagement
{
    [Serializable]
    public class WheelsController
    {
        [SerializeField] private  Wheel[] wheels;

        public void MoveWheels(float direction)
        {
            wheels[0].ApplyMotorTorque(direction * 1500);
            wheels[1].ApplyMotorTorque(direction * 1500);
        }

        public void ApplyBrake(float force = 10000)
        {
            foreach (var wheel in wheels)
            {
                wheel.ApplyBrakeTorque(force);
            }
        }

        public void RotateWheels(float direction)
        {
            wheels[0].RotateWheel(direction);
            wheels[1].RotateWheel(direction);
        }

        public void UpdateWheels()
        {
            foreach (var wheel in wheels)
            {
                wheel.UpdateWheel();
            }
        }
    }
}