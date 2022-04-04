using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Game scenes")]
        [SerializeField] private GameSceneSO persistentScene;
        [SerializeField] private GameSceneSO gameplayScene;
        [SerializeField] private GameSceneSO uiScene;
        [SerializeField] private GameSceneSO joinRoomScene;

        [Header("Events")]
        [SerializeField] private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
        [SerializeField] private VoidEventChannelSO onSceneLoaded;

        private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();
        private List<Scene> _scenesToUnload = new List<Scene>();
        private List<GameSceneSO> _persistentScenes = new List<GameSceneSO>();

        private void OnEnable()
        {
            Debug.Log("Hello");
            loadSceneEvent.OnEventRaised += LoadScene;
            loadMenuSceneEvent.OnEventRaised += LoadMainMenu;
            _persistentScenes.Add(uiScene);
            _persistentScenes.Add(persistentScene);
            _persistentScenes.Add(gameplayScene);
            _persistentScenes.Add(joinRoomScene);
        }

        private void OnDisable()
        {
            loadSceneEvent.OnEventRaised -= LoadScene;
            loadMenuSceneEvent.OnEventRaised -= LoadMainMenu;
        }

        private void LoadMainMenu(GameSceneSO menuScene, bool showLoadingScreen)
        {
            _persistentScenes.Clear();
            _persistentScenes.Add(persistentScene);

            AddScenesToUnload(_persistentScenes);
            UnloadScenes();
            LoadScenes(menuScene, showLoadingScreen);
        }

        private void LoadScene(GameSceneSO sceneToLoad, bool showLoadingScreen)
        {
            _persistentScenes.Clear();
            _persistentScenes.Add(uiScene);
            _persistentScenes.Add(persistentScene);
            _persistentScenes.Add(gameplayScene);
            _persistentScenes.Add(joinRoomScene);

            AddScenesToUnload(_persistentScenes);
            UnloadScenes();
            LoadScenes(sceneToLoad, showLoadingScreen);
        }

        private void AddScenesToUnload(List<GameSceneSO> persistentScenes)
        {
            bool wasPersistent;

            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                string sceneName = scene.name;
                wasPersistent = false;

                for (int j = 0; j < persistentScenes.Count; ++j)
                {
                    if (sceneName.Equals(persistentScenes[j].SceneName))
                    {
                        wasPersistent = true;
                        break;
                    }
                }

                if (!wasPersistent)
                {
                    _scenesToUnload.Add(scene);
                }
            }
        }

        private void LoadScenes(GameSceneSO sceneToLoad, bool showLoadingScreen)
        {
            if (showLoadingScreen)
            {
                // display loading screen
            }

            _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(sceneToLoad.SceneName,
                LoadSceneMode.Additive));
            LoadPersistentScenes();
            StartCoroutine(WaitForSceneLoading(showLoadingScreen));
        }

        private bool CheckIfSceneLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.name.Equals(sceneName))
                {
                    return true;
                }
            }

            return false;
        }
        private void LoadPersistentScenes()
        {
            foreach (var scene in _persistentScenes)
            {
                if (CheckIfSceneLoaded(scene.SceneName) == false)
                {
                    _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(scene.SceneName,
                        LoadSceneMode.Additive));
                }
            }
        }

        private void UnloadScenes()
        {
            foreach (var scene in _scenesToUnload)
            {
                SceneManager.UnloadSceneAsync(scene);
            }

            if (_scenesToUnload != null)
                _scenesToUnload.Clear();
        }

        private IEnumerator WaitForSceneLoading(bool showLoadingScreen)
        {
            var isLoadingDone = false;

            while (!isLoadingDone)
            {
                foreach (var operation in _scenesToLoadAsyncOperations)
                {
                    if (!operation.isDone)
                    {
                        break;
                    }

                    isLoadingDone = true;
                }

                yield return null;
            }

            _scenesToLoadAsyncOperations.Clear();
            _persistentScenes.Clear();
            onSceneLoaded.RaiseEvent();
            LightProbes.TetrahedralizeAsync();

            if (showLoadingScreen)
            {
                // stop displaying loading screen
            }
        }
    }
}