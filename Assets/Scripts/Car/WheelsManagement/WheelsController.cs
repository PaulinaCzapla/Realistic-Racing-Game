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
            
        }

        public void RotateWheels(float direction)
        {
            wheels[0].RotateWheel(direction);
            wheels[1].RotateWheel(direction);
        }
    }
}