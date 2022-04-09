using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Network.NetworkManager
{

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PlayerPrefs.DeleteAll();
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}