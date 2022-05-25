using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInput;
    [SerializeField] private InputField joinInput;
    [SerializeField] private Text connectionStatus;
    
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        connectionStatus.text = "Wait for other players";
        PhotonNetwork.LoadLevel("MultiplayerDemo");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        connectionStatus.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        connectionStatus.text = message;
    }
}
