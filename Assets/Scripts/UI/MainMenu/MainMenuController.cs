using System;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using SoundManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    /// <summary>
    /// Main menu layout controller
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button tutorialButton;
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
        [SerializeField] private GameObject tutorialContainer;
        [SerializeField] private GameObject menuContainer;


        private void OnEnable()
        {
            tutorialButton.onClick.AddListener(HandleTutorialButtonClick);
            roomButton.onClick.AddListener(OnPlayClicked);
            optionsButton.onClick.AddListener(HandleOptionsButtonClick);
            creditsButton.onClick.AddListener(HandleCreditsButtonClick);
            backButton.onClick.AddListener(HandleBackButtonClick);
            onMenuMusicStart.RaiseEvent(SoundName.MenuMusic);
        }

        private void OnPlayClicked()
        {
            loadSceneEvent.RaiseEvent(joinRoomScene, true);
        }

        private void HandleTutorialButtonClick()
        {
            tutorialContainer.SetActive(true);
            menuContainer.SetActive(false);
            backButton.gameObject.SetActive(true);
        }

        private void HandleBackButtonClick()
        {
            tutorialContainer.SetActive(false);
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
            tutorialButton.onClick.RemoveAllListeners();
            roomButton.onClick.RemoveAllListeners();
            optionsButton.onClick.RemoveAllListeners();
            creditsButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }
    }
}
