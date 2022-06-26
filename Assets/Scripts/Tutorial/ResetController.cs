using Events.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for resetting car position in tutorial
/// </summary>
public class ResetController : MonoBehaviour
{
    [SerializeField] private ResetCarEventChannelSO resetCarEvent;
    [SerializeField] private Transform car;

    private Vector3 beginPosition;
    private Quaternion beginRotation;

    private void Start()
    {
        beginPosition = car.position;
        beginRotation = car.rotation;
    }

    private void OnEnable()
    {
        resetCarEvent.OnEventRaised += OnResetCar;
    }
    private void OnDisable()
    {
        resetCarEvent.OnEventRaised -= OnResetCar;
    }

    private void OnResetCar()
    {
        car.position = beginPosition;
        car.rotation = beginRotation;
    }

}
