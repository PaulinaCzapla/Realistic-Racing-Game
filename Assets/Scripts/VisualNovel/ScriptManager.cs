using System;
using System.Collections;
using Events.ScriptableObjects;
using InputSystem;
using UnityEngine;

namespace VisualNovel
{
    public class ScriptManager : MonoBehaviour
    {
        [SerializeField] private UIInputReader input;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;
        
        private ScriptSO _script;
        private int _sceneIndex = 0;
        private void OnEnable()
        {
            input.SkipDialogueEvent += OnSkipClicked;
            onDialogueFinishedEvent.OnEventRaised += OnSceneFinished;
        }

        private void OnDisable()
        {
            input.SkipDialogueEvent -=  OnSkipClicked;
            onDialogueFinishedEvent.OnEventRaised -= OnSceneFinished;
        }

        private void OnSceneFinished()
        {
            _sceneIndex++;
        }

        private void OnSkipClicked()
        {
            displayDialogueSceneEvent.RaiseEvent(_sceneIndex);
        }
    }
}