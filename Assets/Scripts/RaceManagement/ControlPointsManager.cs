using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement
{
    public class ControlPointsManager : MonoBehaviour
    {
        [SerializeField] private int maxLapsCount = 2;
        [SerializeField] private List<ControlPoint> controlPoints;
        [SerializeField] private PhotonView photonView;
        
        [Header("EventChannels")]
        [SerializeField] private RaceParticipantEventChannelSO onFinishPointEntered;
        [SerializeField] private ControlPointEnterEventChannelSO  onControlPointEntered;
        [SerializeField] private IntEventChannelSO onSetMaxLapsCount;
        [SerializeField] private VoidEventChannelSO onRaceFinished;

        private int _index = 0;
        private void OnEnable()
        {
            onFinishPointEntered.OnEventRaised += OnFinishLineAchieved;
            onControlPointEntered.OnEventRaised += OnControlPointEntered;
            onSetMaxLapsCount.RaiseEvent(maxLapsCount);
        }

        private void OnDisable()
        {
            onFinishPointEntered.OnEventRaised -= OnFinishLineAchieved;
            onControlPointEntered.OnEventRaised -= OnControlPointEntered;
        }

        private void OnControlPointEntered(RaceParticipant participant, ControlPoint controlPoint)
        {
            if (!participant.ControlPointsActivated.Contains(controlPoint)
                && _index< controlPoints.Count && controlPoints[_index] == controlPoint)
            {
                participant.ControlPointsActivated.Add(controlPoint);
                _index++;
                Debug.Log("Control point entered");
            }
        }

        private void OnFinishLineAchieved(RaceParticipant participant)
        {
            if (controlPoints.Count == participant.ControlPointsActivated.Count)
            {
                participant.LapFinished();
                
                _index = 0;
                
                if (participant.LapsFinished >= maxLapsCount)
                {
                    // todo: display end view
                    Debug.Log("max lap count achieved");
                }

                if (photonView ? photonView.IsMine : true)
                {
                    //invoke update UI event
                }
            }
        }
    }
}
