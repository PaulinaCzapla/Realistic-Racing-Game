using UnityEngine;

namespace Timers
{
    public class Timer
    {
        public float TimeElapsed => Time.time - _startTime;
        
        private float _startTime;
        
        public void StartTimer()
        {
            _startTime = Time.time;
        }
    }
}