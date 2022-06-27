using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Events.ScriptableObjects;
using Network;
using Photon.Pun;
using RaceManagement.ControlPoints;
using UnityEngine;

namespace RaceManagement
{
    /// <summary>
    /// Class that contains all activated control points and manages it
    /// /// </summary>
    public class ControlPointsManager : MonoBehaviour
    {
        private int _raceLaps;
        public List<string> raceOutcome = new List<string>();
        private Vector3 _afterRacePosition = new Vector3(-10, -16, 0);
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

        /// <summary>
        /// Invoked when race is started, sets first control point
         /// </summary>
        private void OnRaceStarted()
        {
            var participants = FindObjectsOfType<RaceParticipant>();

            foreach (var participant in participants)
            {
                Debug.Log("participant found");
                OnControlPointEntered(participant, controlPoints[0]);
            }
        }

        /// <summary>
        /// Invoked when race is finished, sets time
        /// </summary>
        private void OnRaceFinished(RaceParticipant raceParticipant)
        {
            var timeSpan = TimeSpan.FromSeconds(raceParticipant.RaceTime);
            var raceTime = timeSpan.ToString(@"mm\:ss\:ff");
            raceOutcome.Add(raceParticipant.Name +"   Time: " + raceTime);
        }

        /// <summary>
        /// Checks if activated control point is correct
        /// </summary>
        private void OnControlPointEntered(RaceParticipant participant, ControlPoint controlPoint)
        {
            if (!participant.ControlPointsActivated.Contains(controlPoint)
                && participant.ControlPointsActivated.Count < controlPoints.Count && 
                controlPoints[participant.ControlPointsActivated.Count] == controlPoint)
            {
                participant.ControlPointsActivated.Add(controlPoint);
            }
        }

        /// <summary>
        /// Invoked when player is on the finish line
        /// </summary>
        private void OnFinishLineAchieved(RaceParticipant participant)
        {
            if (controlPoints.Count == participant.ControlPointsActivated.Count)
            {
                participant.LapFinished();
                
                _index = 0;
                
                if (participant.LapsFinished >= _raceLaps) //
                {
                    // todo: display end view
                    onFinish.RaiseEvent(participant);
                    photonView = participant.GetComponent<PhotonView>();
                    
                    if (participant.GetComponent<PhotonView>().IsMine)
                    {
                        finishMenu.gameObject.SetActive(true);
                        var timeSpan = TimeSpan.FromSeconds(participant.RaceTime);
                        var raceTime = timeSpan.ToString(@"mm\:ss\:ff");
                        var nameTime = participant.Name + "   Time: " + raceTime;
                        this.photonView.RPC("ParticipantFinishedRace", RpcTarget.AllBuffered, nameTime);
                        participant.GetComponent<BackToCheckpoint>().StopWheelsAfterFinish();
                        participant.transform.position = _afterRacePosition;
                        participant.GetComponent<DisconnectPlayer>().RaceFinished = true;
                    }
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
