using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement
{
    public class RaceParticipant : MonoBehaviour 
    { 
        public int LapsFinished => _lapsFinished;
        
        //to get last activated control point - ControlPointsActivated[ControlPointsActivated.Count -1].SpawnPoint
        public List<ControlPoint> ControlPointsActivated { get; set; }
        
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;

        private int _lapsFinished = 0;

        private void Awake()
        {
            ControlPointsActivated = new List<ControlPoint>();
        }

        public void LapFinished()
        {
            _lapsFinished++;
            ControlPointsActivated.Clear();
            onUpdateLapsCount.RaiseEvent(_lapsFinished);
            Debug.Log("laps finished: " +_lapsFinished);
        }
    }
}
