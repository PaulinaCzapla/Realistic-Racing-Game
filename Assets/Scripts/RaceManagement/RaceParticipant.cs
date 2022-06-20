using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using RaceManagement.ControlPoints;
using RaceManagement.Timer;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace RaceManagement
{
    public class RaceParticipant : MonoBehaviour 
    { 
        public int LapsFinished => _lapsFinished;
        public string Name; //=> _name;
        public float RaceTime => _stats.RaceTime;
        
        //to get last activated control point - ControlPointsActivated[ControlPointsActivated.Count -1].SpawnPoint
        public List<ControlPoint> ControlPointsActivated { get; set; }
        

        [Header("EventChannels")]
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;
        [SerializeField] private FloatEventChannelSO onUpdateRaceTimeUI;
        [SerializeField] private VoidEventChannelSO onRaceStarted;
        [SerializeField] private VoidEventChannelSO onRaceFinished;

        private int _lapsFinished = 0;
        private string _name;
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
            onRaceFinished.OnEventRaised += StopTimer;
        }

        private void OnDisable()
        {
            onRaceStarted.OnEventRaised -=StartTimer;
            onRaceFinished.OnEventRaised -= StopTimer;
        }

        private void StartTimer()
        {
            _timer.StartTimer();
            _timerStarted = true;
        }

        public void StopTimer()
        {
            _timerStarted = false;
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
