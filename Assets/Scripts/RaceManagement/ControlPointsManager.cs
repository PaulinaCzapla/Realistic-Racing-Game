using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Events.ScriptableObjects;
using Photon.Pun;
using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement
{
    public class ControlPointsManager : MonoBehaviour
    {
        private int _raceLaps;
        public List<string> raceOutcome = new List<string>();
        [SerializeField] private int maxLapsCount = 2;
        [SerializeField] private List<ControlPoint> controlPoints;
        [SerializeField] private PhotonView photonView;
        [SerializeField] private Canvas finishMenu;
        
        [Header("EventChannels")]
        [SerializeField] private RaceParticipantEventChannelSO onFinishPointEntered;
        [SerializeField] private ControlPointEnterEventChannelSO  onControlPointEntered;
        [SerializeField] private IntEventChannelSO onSetMaxLapsCount;
        [SerializeField] private VoidEventChannelSO onRaceFinished;
        [SerializeField] private VoidEventChannelSO onRaceStarted;
        [SerializeField] private RaceParticipantEventChannelSO onFinish;

        private int _index = 0;
        private void OnEnable()
        {
            onFinishPointEntered.OnEventRaised += OnFinishLineAchieved;
            onControlPointEntered.OnEventRaised += OnControlPointEntered;
            onRaceStarted.OnEventRaised += OnRaceStarted;
            onFinish.OnEventRaised += OnRaceFinished;
            _raceLaps = PlayerPrefs.GetInt("NumberOfLaps");
            onSetMaxLapsCount.RaiseEvent(_raceLaps);
        }

        private void OnDisable()
        {
            onFinishPointEntered.OnEventRaised -= OnFinishLineAchieved;
            onControlPointEntered.OnEventRaised -= OnControlPointEntered;
            onRaceStarted.OnEventRaised -= OnRaceStarted;
            onFinish.OnEventRaised -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
            var participants = FindObjectsOfType<RaceParticipant>();

            foreach (var participant in participants)
            {
                Debug.Log("participant found");
                OnControlPointEntered(participant, controlPoints[0]);
            }
        }

        private void OnRaceFinished(RaceParticipant raceParticipant)
        {
            var timeSpan = TimeSpan.FromSeconds(raceParticipant.RaceTime);
            var raceTime = timeSpan.ToString(@"mm\:ss\:ff");
            raceOutcome.Add(raceParticipant.Name +"   Time: " + raceTime);
        }

        private void OnControlPointEntered(RaceParticipant participant, ControlPoint controlPoint)
        {
            if (!participant.ControlPointsActivated.Contains(controlPoint)
                && participant.ControlPointsActivated.Count < controlPoints.Count && controlPoints[participant.ControlPointsActivated.Count] == controlPoint)
            {
                participant.ControlPointsActivated.Add(controlPoint);
            }
        }

        private void OnFinishLineAchieved(RaceParticipant participant)
        {
            if (controlPoints.Count == participant.ControlPointsActivated.Count)
            {
                participant.LapFinished();
                
                _index = 0;
                
                if (participant.LapsFinished >= _raceLaps) //
                {
                    // todo: display end view
                    //onRaceFinished.RaiseEvent();
                    onFinish.RaiseEvent(participant);
                    photonView = participant.GetComponent<PhotonView>();
                    
                    if (participant.GetComponent<PhotonView>().IsMine)
                    {
                        finishMenu.gameObject.SetActive(true);
                        var timeSpan = TimeSpan.FromSeconds(participant.RaceTime);
                        var raceTime = timeSpan.ToString(@"mm\:ss\:ff");
                        var nameTime = participant.Name + "   Time: " + raceTime;
                        this.photonView.RPC("ParticipantFinishedRace", RpcTarget.AllBuffered, nameTime);
                    }
                    //finishMenu.gameObject.SetActive(true);
                    Debug.Log("max lap count achieved");
                }
                
                
                if (photonView ? photonView.IsMine : true)
                {    
                    //invoke update UI even
                }
            }
        }

        [PunRPC]
        public void ParticipantFinishedRace(string nameTime)
        {
            raceOutcome.Add(nameTime);
        }
    }
}
