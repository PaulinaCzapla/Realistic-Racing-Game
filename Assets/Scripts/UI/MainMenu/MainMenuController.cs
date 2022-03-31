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
        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private GameSceneSO RaceTrackScene;
        private void OnEnable()
        {
            startButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(RaceTrackScene, true));
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
        }
    }
}
