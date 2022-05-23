using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public Text connectionStatus;
    void Start()
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
