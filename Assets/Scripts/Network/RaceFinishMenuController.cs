using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using RaceManagement;
using SceneManagement.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class RaceFinishMenuController : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> ranking = new List<TextMeshProUGUI>();
        [SerializeField] private ControlPointsManager controlPointsManager;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private GameSceneSO mainMenuScene;
        [SerializeField] private Button backButton;

        private void OnEnable()
        {
            backButton.onClick.AddListener(LeaveGame);
        }

        private void Update()
        {
            for (var i = 0; i < controlPointsManager.raceOutcome.Count; i++)
            {
                ranking[i].gameObject.SetActive(true);
                ranking[i].transform.Find("NameTime").GetComponent<TextMeshProUGUI>().text =
                    controlPointsManager.raceOutcome[i];
            }
        }

        private void LeaveGame()
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            loadMenuSceneEvent.RaiseEvent(mainMenuScene, true);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
        }
    }
}
