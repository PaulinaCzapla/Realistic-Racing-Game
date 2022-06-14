using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using InputSystem;
using Photon.Pun;
using RaceManagement.ResetDetection;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class LeaveGame : MonoBehaviour
{
    [SerializeField] private GameplayInputReader inputReader;
    [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
    [SerializeField] private GameSceneSO mainMenuScene;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    private void OnEnable()
    {
        yesButton.onClick.AddListener(YesLeaveGame);
        noButton.onClick.AddListener(NoLeaveGame);
        inputReader.LeaveGameEvent += ShowQuestionPanel;
    }

    private void OnDisable()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        inputReader.ResetPositionEvent -= ShowQuestionPanel;
    }

    private void ShowQuestionPanel()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
           questionPanel.SetActive(true);
            gameObject.GetComponent<BackToCheckpoint>().StopWheelsAfterFinish(); 
        }
    }

    private void YesLeaveGame()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
    }

    private void NoLeaveGame()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            questionPanel.SetActive(false);
        }
    }
}
