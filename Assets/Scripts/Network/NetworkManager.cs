using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Network.NetworkManager
{

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Button joinRoomButton;
        [SerializeField]
        private Button goToGameButton;
        [SerializeField]
        private Text connectionStatus;

        private void Start()
        {
            PlayerPrefs.DeleteAll();
            connectionStatus.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings(); //2
            PhotonNetwork.GameVersion = "1.0";
            goToGameButton.gameObject.SetActive(false);
        }
        // Photon Methods
        public override void OnConnected()
        {
            // 1
            base.OnConnected();
            // 2
            connectionStatus.text = "Connected to Photon!";
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError("Disconnected from server because " + cause.ToString());
        }

        public override void OnJoinedRoom()
        {
            // 4
            if (PhotonNetwork.IsMasterClient)
            {
                goToGameButton.gameObject.SetActive(true);
                joinRoomButton.gameObject.SetActive(false);
                connectionStatus.text = "You are Lobby Leader";
            }
            else
            {
                connectionStatus.text = "Connected to Lobby";
            }
        }

    }
}