using System;
using System.Collections.Generic;
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

        public InputField roomName;
        List<RoomInfo> Rooms = new List<RoomInfo>();
        private void Start()
        {
            goToGameButton.gameObject.SetActive(false);
            connectionStatus.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            connectionStatus.text = "Connected to Photon!";
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(roomName.text);
            connectionStatus.text = "Room " + roomName.text + " created!";
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(roomName.text);
        }

        public override void OnJoinedRoom()
        {
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
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var item in roomList)
            {
                if (!item.IsVisible || !item.IsOpen || item.RemovedFromList)
                    continue;
 
                Rooms.Add(item);
            }
        }
        /*private void Awake()
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
        }*/

    }
}