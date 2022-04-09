using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Network.RoomConnectionManager
{

    public class RoomConnectionManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Button joinRoomButton;
        [SerializeField]
        private Button goToGameButton;
        [SerializeField]
        private Text connectionStatus;
        private void Awake()
        {
            goToGameButton.gameObject.SetActive(false);
        }
        private void Start()
        {
            goToGameButton.gameObject.SetActive(false);
            if (PhotonNetwork.IsConnected) return;
            PlayerPrefs.DeleteAll();
            connectionStatus.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1.0";
        }
        // Photon Methods
        public override void OnConnected()
        {
            base.OnConnected();
            connectionStatus.text = "Connected to Photon!";
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError("Disconnected from server because " + cause.ToString());
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("weszlem");
            if (PhotonNetwork.IsMasterClient)
            {
                goToGameButton.gameObject.SetActive(true);
                joinRoomButton.gameObject.SetActive(false);
                connectionStatus.text = "You are Lobby Leader";
            }
            else
            {
                goToGameButton.gameObject.SetActive(true);
                joinRoomButton.gameObject.SetActive(false);
                connectionStatus.text = "Connected to Lobby";
            }
        }

    }
}