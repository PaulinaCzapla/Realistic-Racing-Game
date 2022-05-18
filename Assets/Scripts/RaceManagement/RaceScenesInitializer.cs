using System;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaceManagement
{
    public class RaceScenesInitializer : MonoBehaviour
    {
        [SerializeField] private GameSceneSO UI;
        [SerializeField] private GameSceneSO persistentScene;

        private void OnEnable()
        {
             SceneManager.LoadSceneAsync(UI.SceneName, LoadSceneMode.Additive);
             SceneManager.LoadSceneAsync(persistentScene.SceneName, LoadSceneMode.Additive);
        }
    }
}