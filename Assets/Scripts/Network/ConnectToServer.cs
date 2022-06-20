using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text connectionStatus;
        [SerializeField] private Button backButton;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private GameSceneSO mainMenuScene;

        public void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            backButton.onClick.AddListener(ReturnToMenu);
        }

        public override void OnConnectedToMaster()
        {
            connectionStatus.text = "Connected to Master";
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            connectionStatus.text = "Joined Lobby";
        }

        private void ReturnToMenu()
        {
            PhotonNetwork.Disconnect();
            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
        }
    }
}
