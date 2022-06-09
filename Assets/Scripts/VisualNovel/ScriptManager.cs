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
        [SerializeField] private ScriptInfoSO scriptInfo;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;
        
        private ScriptSO _script;
       // private int _sceneIndex = 0;

       private IEnumerator Start()
       {
           input.SetInput();
           yield return new WaitForSeconds(1.5f);
           displayDialoguePanelEvent.RaiseEvent(scriptInfo.CurrentlySelectedScript, scriptInfo.CurrentDialogueScene);

       }

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
            Debug.Log("finished");
           // scriptInfo.CurrentDialogueScene++;
        }

        private void OnSkipClicked()
        {
            Debug.Log("clicked");
            displayDialogueSceneEvent.RaiseEvent(scriptInfo.CurrentDialogueScene);
        }
    }
}