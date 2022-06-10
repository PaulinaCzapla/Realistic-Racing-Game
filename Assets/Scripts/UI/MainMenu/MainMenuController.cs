using System;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using SoundManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button roomButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button backButton;

        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private SoundEventChannelSO onMenuMusicStart;

        //[SerializeField] private GameSceneSO RaceTrackScene;
        [SerializeField] private GameSceneSO joinRoomScene;
        [SerializeField] private GameObject optionsContainer;
        [SerializeField] private GameObject creditsContainer;
        [SerializeField] private GameObject menuContainer;

        private void OnStart()
        {
            backButton.gameObject.SetActive(false);
        }
        private void OnAwake()
        {

        }
        private void OnEnable()
        {
            //  startButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(RaceTrackScene, true));
            roomButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(joinRoomScene, true));
            optionsButton.onClick.AddListener(HandleOptionsButtonClick);
            creditsButton.onClick.AddListener(HandleCreditsButtonClick);
            backButton.onClick.AddListener(HandleBackButtonClick);
            onMenuMusicStart.RaiseEvent(SoundName.MenuMusic);
        }

        private void HandleBackButtonClick()
        {
            optionsContainer.SetActive(false);
            creditsContainer.SetActive(false);
            menuContainer.SetActive(true);
            backButton.gameObject.SetActive(false);
        }
        private void HandleOptionsButtonClick()
        {
            optionsContainer.SetActive(true);
            menuContainer.SetActive(false);
            backButton.gameObject.SetActive(true);
        }

        private void HandleCreditsButtonClick()
        {
            creditsContainer.SetActive(true);
            menuContainer.SetActive(false);
            backButton.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
            roomButton.onClick.RemoveAllListeners();
            optionsButton.onClick.RemoveAllListeners();
            creditsButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}
