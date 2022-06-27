using UnityEngine;

namespace Timers
{
    /// <summary>
    /// Class for counting cooldown.
    /// /// </summary>
    public class Cooldown
    {
        public bool CooldownEnded => IsCooldownEnded();

        private readonly float _cooldownTime;
        private float _nextFireTime;
        private bool _isStarted;
        public bool IsStarted => _isStarted;

        public Cooldown(float cooldownTime, bool startWhenInit = false)
        {
            _cooldownTime = cooldownTime;
            
            if(startWhenInit)
                StartCooldown();
        }

        /// <summary>
        /// Starts time counting
        /// </summary>
        public void StartCooldown()
        {
            _nextFireTime = Time.time + _cooldownTime;
            _isStarted = true;
        }

        /// <summary>
        /// Returns information about cooldown finish
        /// </summary>
        private bool IsCooldownEnded()
        {
            if (Time.time > _nextFireTime)
            {
                _isStarted = false;
            }

            return Time.time > _nextFireTime;
        }
    }
}