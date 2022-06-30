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
        [SerializeField] private CarSO carSo;
        

        private const float BrakeForce = 8000;
        private const float Rate = 45;
        private float _angle;
        
        /// <summary>
        /// Adds power to the wheels as torque
        /// </summary>
        /// <param name="direction">
        /// Input provided <0 when in reverse
        /// </param>
        /// <param name="motorForce">
        /// Sum of the wheels torques that needs to be distributed
        /// </param>
        public void MoveWheels(float direction, float motorForce)
        {

            //apply motor force to wheels depending on drive
            switch (carSo.drive)
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


        /// <summary>
        /// Handles brakes
        /// </summary>
        /// <param name="force">
        /// The break torque
        /// </param>
        public void ApplyBrake(float force = BrakeForce )
        {
            //apply brake force to all wheels
            foreach (var wheel in wheels)
            {
                wheel.ApplyBrakeTorque(force);
            }
        }

        /// <summary>
        /// Stopping wheels procedure
        /// </summary>
        public void StopWheels()
        {
            //stop all wheels
            foreach (var wheel in wheels)
            {
                wheel.ApplyMotorTorque(0);
            }
        }
        
        /// <summary>
        /// Handles correct rotation of the wheel
        /// </summary>
        /// <param name="steeringInput">
        /// Player input as float <0 when left
        /// </param>
        /// <param name="maxAngle">
        /// Max angle that wheel can rotate at given speed
        /// </param>
        public void RotateWheels(float steeringInput, float maxAngle)
        {
            var destination = steeringInput * maxAngle;
            float currAngle = 0;
            currAngle = Mathf.MoveTowards(currAngle, destination,   Rate);
            currAngle = Mathf.Clamp(currAngle, -maxAngle, maxAngle);
            _angle = currAngle;
            carSo.currentSteerAngle = _angle;
        }

        /// <summary>
        /// Updates the wheel positions, it provides Ackermann steering
        /// </summary>
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