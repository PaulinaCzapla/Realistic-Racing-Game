using System;
using System.Collections;
using System.Collections.Generic;
using Car.WheelsManagement;
using InputSystem;
using Photon.Pun;
using RaceManagement;
using UnityEngine;
using UnityEngine.UI;
using RaceManagement.ControlPoints;

public class BackToCheckpoint : MonoBehaviour
{
    //[SerializeField] private Button resetButton;
    [SerializeField] private CarSO car;
    [SerializeField] private GameplayInputReader inputReader;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private WheelsController wheelsController = new WheelsController();
    private ControlPoint _pointcontrol;
    private RaceParticipant _raceParticipant;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        inputReader.ResetPositionEvent += ResetPosition;
    }

    private void OnDisable()
    {
        inputReader.ResetPositionEvent -= ResetPosition;
    }

    void Start()
    {
        _raceParticipant = GetComponent<RaceParticipant>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void ResetPosition()
    {
        if (photonView ? photonView.IsMine : true)
        {
            if (_raceParticipant.ControlPointsActivated.Count > 0)
            {
                //wheelsController.ApplyBrake();
                wheelsController.StopWheels();
                _pointcontrol =
                    _raceParticipant.ControlPointsActivated[_raceParticipant.ControlPointsActivated.Count - 1];
                var transform1 = transform;
                _rigidbody.isKinematic = true;
                transform1.rotation = _pointcontrol.SpawnPoint.transform.rotation;
                transform1.position = _pointcontrol.SpawnPoint.transform.position;
                car.gearNum = 1;
                StartCoroutine(inputReader.DisableInput());
                _rigidbody.isKinematic = false;
            }
        }
    }
}
