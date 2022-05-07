using System;
using UnityEngine;
using Utilities;

namespace Car.WheelsManagement
{
    [Serializable]
    public class WheelsController
    {
        public float Wheel0RPM => wheels[0].WheelRPM;
        public float Wheel2RPM => wheels[2].WheelRPM;
        
        public float AxleSeparation => (wheels[1].transform.position - wheels[3].transform.position).magnitude;
        public float AxleWidth => (wheels[1].transform.position - wheels[0].transform.position).magnitude;
        

        [SerializeField] private  Wheel[] wheels;

        //tmp values for testing
        //private const float MotorForce = 1500;
        private const float BrakeForce = 10000;
        private const float Range = 35;
        private const float Rate = 45;
        private float _angle;
        
        
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

        public void RotateWheels(float steeringInput)
        {
            var destination = steeringInput * Range;
            float currAngle = 0;
            currAngle = Mathf.MoveTowards(currAngle, destination,   Rate);
            currAngle = Mathf.Clamp(currAngle, -Range, Range);
            _angle = currAngle;
        }

        public void UpdateWheels()
        {
            foreach (var wheel in wheels)
            {
                wheel.UpdateWheel();
            }
            
            var farAngle = AckermannUtility.GetSecondaryAngle(_angle, AxleSeparation, AxleWidth);
            // The rear wheels are always at 0 steer in Ackermann
            wheels[2].SetSteeringAngle(0);
            wheels[3].SetSteeringAngle(0);

            if (Mathf.Approximately(_angle, 0))
            {
                wheels[0].SetSteeringAngle(0);
                wheels[1].SetSteeringAngle(0);
            }
            
            wheels[0].SetSteeringAngle(farAngle);
            wheels[1].SetSteeringAngle(_angle);
        }
    }
}