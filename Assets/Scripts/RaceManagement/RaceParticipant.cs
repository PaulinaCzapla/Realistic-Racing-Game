using System;
using System.Collections.Generic;
using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement
{
    public class RaceParticipant : MonoBehaviour
    {
        public int LapsFinished => _lapsFinished;
        
        //to get last activated control point - ControlPointsActivated[ControlPointsActivated.Count -1].SpawnPoint
        public List<ControlPoint> ControlPointsActivated { get; set; }
        private int _lapsFinished = 0;

        private void Awake()
        {
            ControlPointsActivated = new List<ControlPoint>();
        }

        public void LapFinished()
        {
            _lapsFinished++;
            ControlPointsActivated.Clear();
            Debug.Log("laps finished: " +_lapsFinished);
        }
    }
}
