using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    ///<summary>
    /// This class allows player to create new room or join an existing one. When player is connected to the room a next scene is loaded (Color Lobby). On failed action player receives a message about the error.
    ///</summary>
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        [SerializeField] private InputField createInput;
        [SerializeField] private InputField joinInput;
        [SerializeField] private Text connectionStatus;
    
        [SerializeField] private LoadSceneEventChannelSO photonLoadSceneEvent;
        [SerializeField] private GameSceneSO colorLobbyScene;
    
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
            connectionStatus.text = "Please wait...";
            PhotonNetwork.LoadLevel("ColorLobby");
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
}
