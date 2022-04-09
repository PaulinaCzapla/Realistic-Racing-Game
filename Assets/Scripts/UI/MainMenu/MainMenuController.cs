using System;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button roomButton;
        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private GameSceneSO RaceTrackScene;
        [SerializeField] private GameSceneSO JoinRoomScene;

        private void OnEnable()
        {
            startButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(RaceTrackScene, true));
            roomButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(JoinRoomScene, true));
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
            roomButton.onClick.RemoveAllListeners();
        }
    }
}
