using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Network
{
    public class DisconnectPlayer : MonoBehaviour
    {
        public float timeSinceNoInput;
        public float _timeToDisconnecting = 45;
        public bool startCountingNoInput;
        public bool countdownStarted;
        private bool _leaving;
        //private Cooldown _cooldown = new Cooldown(45f, true);
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private VoidEventChannelSO onRaceStarted;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private GameSceneSO mainMenuScene;
    
        private void OnEnable()
        {
            onRaceStarted.OnEventRaised += StartControl;
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
                    timeSinceNoInput += Time.deltaTime;
                    if (timeSinceNoInput > 15f)
                    {
                        infoPanel.SetActive(true);
                        countdownStarted = true;
                    }
                    else if (timeSinceNoInput < 15f)
                    {
                        infoPanel.SetActive(false);
                        countdownStarted = false;
                    }
                }

                if (countdownStarted)
                {
                    if (_timeToDisconnecting > 0)
                    {
                        _timeToDisconnecting -= Time.deltaTime;
                        infoText.text = "No input detected. " + (int) _timeToDisconnecting +
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
                    _timeToDisconnecting = 45f;
                }
            }
        }

        private void OnDisable()
        {
            _timeToDisconnecting = 45;    
            onRaceStarted.OnEventRaised -= StartControl;
        }

    
    
    }
}
