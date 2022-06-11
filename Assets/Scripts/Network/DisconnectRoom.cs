using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class DisconnectRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button menuReturnButton;
    [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
    [SerializeField] private GameSceneSO mainMenuScene;
    private void Start()
    {
        menuReturnButton.onClick.AddListener(ReturnMenu);
    }

    private void ReturnMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        //loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
    }

    public override void OnLeftRoom()
    {
        loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
        base.OnLeftRoom();
    }
}
