using System;
using Events.ScriptableObjects;
using UnityEngine;

namespace UI.HudUI
{
    public class RaceLapsUIController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField] private IntEventChannelSO onSetMaxLapsCount;
        [SerializeField] private VoidEventChannelSO onRaceFinished;
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;

        private int _maxLapsCount;
        private int _lapsCount;

        private void OnEnable()
        {
            onSetMaxLapsCount.OnEventRaised += (int value) => _maxLapsCount = value;
            onUpdateLapsCount.OnEventRaised += (int value) => _lapsCount = value;
        }

        private void OnDisable()
        {
            onSetMaxLapsCount.OnEventRaised -= (int value) => _maxLapsCount = value;
            onUpdateLapsCount.OnEventRaised -= (int value) => _lapsCount = value;
        }
    }
}