using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using Photon.Realtime;
using SceneManagement.ScriptableObjects;
using SoundManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.RoomMenu
{
    public class RoomMenuController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private LoadSceneEventChannelSO loadMenuEvent;

        [Header("Room SO")]
        [SerializeField] private GameSceneSO menu;


        [SerializeField] private InputField nameInput;
        [SerializeField] private InputField roomNameInput;
        [SerializeField] private Button joinRoomButton;
        [SerializeField] private Button goToGameButton;
        [SerializeField] private Button goBackButton;
        private void OnEnable()
        {
            joinRoomButton.onClick.AddListener(() => JoinRoomEvent());
            goToGameButton.onClick.AddListener(() => GoToGameEvent());
            goBackButton.onClick.AddListener(() => loadMenuEvent.RaiseEvent(menu, true));
        }

        private void OnDisable()
        {
            joinRoomButton.onClick.RemoveAllListeners();
            goToGameButton.onClick.RemoveAllListeners();
            goBackButton.onClick.RemoveAllListeners();
        }
        private void JoinRoomEvent()
        {
            if (PhotonNetwork.IsConnected)
            {
                var roomName = roomNameInput.text;
                PhotonNetwork.LocalPlayer.NickName = nameInput.text;
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomName);
                RoomOptions roomOptions = new RoomOptions();
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
        }

        private void GoToGameEvent()
        {
            PhotonNetwork.LoadLevel("MultiplayerDemo");
        }
    }
}