using Photon.Pun;
using UnityEngine.UI;

namespace Network
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        public Text connectionStatus;
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            connectionStatus.text = "Connected to Master";
        }

        public override void OnJoinedLobby()
        { 
            connectionStatus.text = "Joined Lobby";
        }
    }
}
