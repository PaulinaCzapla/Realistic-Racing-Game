using System.Collections;
using System.Collections.Generic;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorLobbyInitializer : MonoBehaviour
{
    [SerializeField] private GameSceneSO persistentScene;

    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(persistentScene.SceneName, LoadSceneMode.Additive);
    }
}
