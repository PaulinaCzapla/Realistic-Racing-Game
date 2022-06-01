using System;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.HudUI
{
    public class RaceLapsUIController : MonoBehaviour
    {
        [Header("Event Channels")] [SerializeField]
        private IntEventChannelSO onSetMaxLapsCount;

        [SerializeField] private VoidEventChannelSO onRaceFinished;
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;

        [SerializeField] int UpdateFrameCount = 3;
        [SerializeField] TextMeshProUGUI SpeedText;
        [SerializeField] TextMeshProUGUI CurrentGearText;
        [SerializeField] TextMeshProUGUI lapsText;
        
        [SerializeField] RectTransform TahometerArrow;
        [SerializeField] float MinArrowAngle = 0;
        [SerializeField] float MaxArrowAngle = -315f;
        [SerializeField] private CarSO car;

        //  CarSO SelectedCar { get { return GameController.PlayerCar; } }

        private int _maxLapsCount;
        private int _lapsCount;
        private int _currentFrame;
        
        private void FixedUpdate()
        {
            // if (_currentFrame >= UpdateFrameCount)
            // {
            //     UpdateGamePanel();
            //     _currentFrame = 0;
            // }
            // else
            // {
            //     _currentFrame++;
            // }
            UpdateGamePanel();
            UpdateArrow();
            UpdateLaps();
        }

        void UpdateArrow()
        {
            var procent = 0.3f *(car.engineRpm/ car.MAXRpm);
            var angle = (MaxArrowAngle - MinArrowAngle) * procent + MinArrowAngle;
            TahometerArrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        void UpdateGamePanel()
        {
            SpeedText.text = car.carSpeed.ToString("000.0");
            CurrentGearText.text = car.gearNum.ToString();
        }

        private void UpdateLaps()
        {
            lapsText.text = _lapsCount + "/" + 5;
        }
        
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