using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using UnityEngine.SceneManagement;

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
        UnloadActiveScenes();
        PhotonNetwork.LoadLevel("ColorLobby");
        SceneManager.LoadSceneAsync("PersistentScene", LoadSceneMode.Additive);
        //photonLoadSceneEvent.RaiseEvent();
        //PhotonNetwork.LoadLevel("MultiplayerDemo");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        connectionStatus.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        connectionStatus.text = message;
    }

    private void UnloadActiveScenes()
    {
        var countLoaded = SceneManager.sceneCount;

        for (var i = 0; i < countLoaded; i++)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
        
    }
}
