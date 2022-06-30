using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    /// <summary>
    /// A class that is responsible for initializing scenes at the beginning of the game
    /// /// </summary>
    public class SceneInitializer : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private LoadSceneEventChannelSO loadMenuSceneEvent;
    
        [Header("Scenes to load")] 
        [SerializeField] private GameSceneSO mainMenuScene;
        [SerializeField] private GameSceneSO persistentScene;
        
        private Scene _initializationScene;
        private void Awake()
        {
            _initializationScene = SceneManager.GetActiveScene();
            
            SceneManager.LoadSceneAsync("PersistentScene", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            
            // TODO: loading screen and waiting for scene load
            
            SceneManager.UnloadSceneAsync(_initializationScene);
        }
    }
}
