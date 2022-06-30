using UnityEngine;

namespace Timers
{
    /// <summary>
    /// A class responsible for counting time
    /// </summary>
    public class Timer
    {
        public float TimeElapsed => Time.time - _startTime;
        
        private float _startTime;
        
        /// <summary>
        /// Starts time counting
        /// </summary>
        public void StartTimer()
        {
            _startTime = Time.time;
        }
    }
}