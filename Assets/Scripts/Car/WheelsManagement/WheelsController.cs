using System;
using UnityEngine;

namespace Car.WheelsManagement
{
    [Serializable]
    public class WheelsController
    {
        [SerializeField] private  Wheel[] wheels;

        //tmp values for testing
        private const float MotorForce = 1500;
        private const float BrakeForce = 10000;
        
        public void MoveWheels(float direction)
        {
            //apply motor force to front wheels
            wheels[0].ApplyMotorTorque(direction * MotorForce);
            wheels[1].ApplyMotorTorque(direction * MotorForce);
        }

        public void ApplyBrake(float force = BrakeForce )
        {
            //apply brake force to all wheels
            foreach (var wheel in wheels)
            {
                wheel.ApplyBrakeTorque(force);
            }
        }

        public void RotateWheels(float direction)
        {
            //rotate front wheels for moving direction change
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