using UnityEngine;

namespace RaceManagement.Timer
{
    public class TimeCounter
    {
        public float TimeElapsed => Time.time - _startTime;
        
        private float _startTime;
        
        public void StartTimer()
        {
            _startTime = Time.time;
        }
    }
}