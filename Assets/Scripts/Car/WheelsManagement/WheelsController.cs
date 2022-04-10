using System;
using UnityEngine;

namespace Car.WheelsManagement
{
    [Serializable]
    public class WheelsController
    {
        public float Wheel0RPM => wheels[0].WheelRPM;
        public float Wheel2RPM => wheels[2].WheelRPM;

        [SerializeField] private  Wheel[] wheels;
        

        //tmp values for testing
        //private const float MotorForce = 1500;
        private const float BrakeForce = 10000;
        
        public void MoveWheels(float direction, float motorForce, DriveType drive)
        {

            //apply motor force to wheels
            switch (drive)
            {
                case DriveType.RWD:
                    wheels[2].ApplyMotorTorque(direction * motorForce/2);
                    wheels[3].ApplyMotorTorque(direction * motorForce/2);
                    break;
                case DriveType.FWD:
                    wheels[0].ApplyMotorTorque(direction * motorForce/2);
                    wheels[1].ApplyMotorTorque(direction * motorForce/2);
                    break;
                case DriveType.AWD:
                    wheels[0].ApplyMotorTorque(direction * motorForce/4);
                    wheels[1].ApplyMotorTorque(direction * motorForce/4);
                    wheels[2].ApplyMotorTorque(direction * motorForce/4);
                    wheels[3].ApplyMotorTorque(direction * motorForce/4);
                    break;
                default:
                    break;
            }
           
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