using Events.ScriptableObjects;
using InputSystem;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class DisconnectPlayer : MonoBehaviour
    {
        public float TimeSinceNoInput { get; set; }
        public float TimeToDisconnecting { get; set; }
        public bool startCountingNoInput;
        public bool countdownStarted;
        private bool _leaving;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private VoidEventChannelSO onRaceStarted;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private GameSceneSO mainMenuScene;
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private GameObject questionPanel;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
    
        private void OnEnable()
        {
            onRaceStarted.OnEventRaised += StartControl;
            yesButton.onClick.AddListener(YesLeaveGame);
            noButton.onClick.AddListener(NoLeaveGame);
            inputReader.LeaveGameEvent += ShowQuestionPanel;
            TimeToDisconnecting = 45;
        }

        private void StartControl()
        {
            startCountingNoInput = true;
        }

        private void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                if (startCountingNoInput)
                {
                    TimeSinceNoInput += Time.deltaTime;
                    if (TimeSinceNoInput > 15f)
                    {
                        infoPanel.SetActive(true);
                        countdownStarted = true;
                    }
                    else if (TimeSinceNoInput < 15f)
                    {
                        infoPanel.SetActive(false);
                        countdownStarted = false;
                    }
                }

                if (countdownStarted)
                {
                    if (TimeToDisconnecting > 0)
                    {
                        TimeToDisconnecting -= Time.deltaTime;
                        infoText.text = "No input detected. " + (int) TimeToDisconnecting +
                                        " seconds left before removing the player from the room.";
                    }
                    else
                    {
                        if (!_leaving)
                        {
                            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
                            PhotonNetwork.LeaveRoom();
                            PhotonNetwork.Disconnect();
                            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
                            _leaving = true;
                        }
                    }
                }
                else
                {
                    TimeToDisconnecting = 45f;
                }
            }
        }

        private void OnDisable()
        {
            TimeToDisconnecting = 45;    
            onRaceStarted.OnEventRaised -= StartControl;
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            inputReader.ResetPositionEvent -= ShowQuestionPanel;
        }

        private void ShowQuestionPanel()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                questionPanel.SetActive(true);
                gameObject.GetComponent<BackToCheckpoint>().StopWheelsAfterFinish(); 
            }
        }

        private void YesLeaveGame()
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
        }

        private void NoLeaveGame()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                questionPanel.SetActive(false);
            }
        }
    
    
    }
}
