using UnityEngine;

namespace RaceManagement.Timer
{
    /// <summary>
    /// Helper class for time counting.
    /// /// </summary>
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