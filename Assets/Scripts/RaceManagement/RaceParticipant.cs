using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using RaceManagement.ControlPoints;
using RaceManagement.Timer;
using TMPro;
using UnityEngine;

namespace RaceManagement
{
    public class RaceParticipant : MonoBehaviour 
    { 
        public int LapsFinished => _lapsFinished;
        
        //to get last activated control point - ControlPointsActivated[ControlPointsActivated.Count -1].SpawnPoint
        public List<ControlPoint> ControlPointsActivated { get; set; }
        
        [Header("EventChannels")]
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;
        [SerializeField] private FloatEventChannelSO onUpdateRaceTimeUI;
        [SerializeField] private VoidEventChannelSO onRaceStarted;

        private int _lapsFinished = 0;
        private TimeCounter _timer = new TimeCounter();
        private RaceStats _stats = new RaceStats();
        private bool _timerStarted;
        
        private void Awake()
        {
            ControlPointsActivated = new List<ControlPoint>();
        }

        private void OnEnable()
        {
            onRaceStarted.OnEventRaised +=StartTimer;
        }

        private void OnDisable()
        {
            onRaceStarted.OnEventRaised -=StartTimer;
        }

        private void StartTimer()
        {
            _timer.StartTimer();
            _timerStarted = true;
        }
        private void FixedUpdate()
        {
            if (_timerStarted)
            {
                onUpdateRaceTimeUI.RaiseEvent(_timer.TimeElapsed);
                _stats.RaceTime = _timer.TimeElapsed;
            }
        }

        public void LapFinished()
        {
            _stats.LapTimes.Add( _lapsFinished == 0 ? _timer.TimeElapsed :
                _timer.TimeElapsed - _stats.LapTimes[_lapsFinished-1]);
            
            _lapsFinished++;
            ControlPointsActivated.Clear();
            onUpdateLapsCount.RaiseEvent(_lapsFinished);
        }
    }

    public class RaceStats
    {
        public List<float> LapTimes { get; private set; }
        public float RaceTime { get; set; }

        public RaceStats()
        {
            LapTimes = new List<float>();
            RaceTime = 0;
        }
    }
}
