using System;
using Car.WheelsManagement;
using Events.ScriptableObjects;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.HudUI
{
    public class RaceLapsUIController : MonoBehaviour
    {
        private int _raceLaps;
        [Header("Event Channels")] [SerializeField]
        private IntEventChannelSO onSetMaxLapsCount;

        [SerializeField] private VoidEventChannelSO onRaceFinished;
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;

        [SerializeField] int UpdateFrameCount = 3;
        //[SerializeField] private TextMeshProUGUI numberOfPlayersInRace;
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

        /*private void Update()
        {
            var numberOfPlayersInScene = FindObjectsOfType<CarMovementController>();
            numberOfPlayersInRace.text = numberOfPlayersInScene.Length + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        }*/

        private void FixedUpdate()
        {
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
            SpeedText.text = ((int)car.carSpeed).ToString();
            CurrentGearText.text = car.gearNum.ToString();
        }

        private void UpdateLaps()
        {
            lapsText.text = _lapsCount + "/" + _raceLaps;
        }
        
        private void OnEnable()
        {
            _raceLaps = PlayerPrefs.GetInt("NumberOfLaps");
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