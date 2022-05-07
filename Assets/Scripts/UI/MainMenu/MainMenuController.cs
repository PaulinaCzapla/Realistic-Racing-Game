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
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private GameSceneSO RaceTrackScene;
        [SerializeField] private GameSceneSO JoinRoomScene;
        [SerializeField] private GameObject OptionsContainer;
        [SerializeField] private GameObject CreditsContainer;
        [SerializeField] private GameObject MenuContainer;

        private void OnStart()
        {
            backButton.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            startButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(RaceTrackScene, true));
            roomButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(JoinRoomScene, true));
            optionsButton.onClick.AddListener(() => handleOptionsButtonClick());
            creditsButton.onClick.AddListener(() => handleCreditsButtonClick());
            backButton.onClick.AddListener(() => handleBackButtonClick());
        }

        private void handleBackButtonClick()
        {
            OptionsContainer.SetActive(false);
            CreditsContainer.SetActive(false);
            MenuContainer.SetActive(true);
            backButton.gameObject.SetActive(false);
        }
        private void handleOptionsButtonClick()
        {
            OptionsContainer.SetActive(true);
            MenuContainer.SetActive(false);
            backButton.gameObject.SetActive(true);
        }

        private void handleCreditsButtonClick()
        {
            CreditsContainer.SetActive(true);
            MenuContainer.SetActive(false);
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
