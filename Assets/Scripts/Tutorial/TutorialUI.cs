using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using SceneManagement;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VisualNovel;

/// <summary>
/// Manages tutorial ui and sets adequate tutorial script.
/// </summary>
public class TutorialUI : MonoBehaviour
{
    [Header("Scriptable objects")]
    [SerializeField] private ScriptInfoSO scriptInfo;
    [SerializeField] private ScriptSO scriptGirls;
    [SerializeField] private ScriptSO scriptBoys;
    [SerializeField] private GameSceneSO tutorialScene;
    [SerializeField] private GameSceneSO mainMenuScene;
    [SerializeField] private GameSceneSO persistentScene;

    [Header("Buttons")] 
    [SerializeField] private Button buttonGirls;
    [SerializeField] private Button buttonBoys;

    private void OnEnable()
    {
        buttonGirls.onClick.AddListener( () => OnButtonClicked(scriptGirls));
        buttonBoys.onClick.AddListener( () => OnButtonClicked(scriptBoys));
    }

    private void OnDisable()
    {
        buttonGirls.onClick.RemoveAllListeners();
        buttonBoys.onClick.RemoveAllListeners();
    }

    private void OnButtonClicked(ScriptSO script)
    {  
        scriptInfo.CurrentlySelectedScript = script;

        var loadScene = SceneManager.LoadSceneAsync(tutorialScene.SceneName, LoadSceneMode.Additive);
        loadScene.completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
