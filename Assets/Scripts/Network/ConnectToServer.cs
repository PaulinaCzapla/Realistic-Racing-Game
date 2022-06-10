using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text connectionStatus;

        public void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
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
    }
}
