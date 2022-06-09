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
        
       private IEnumerator Start()
       {
           input.SetInput();
           if (scriptInfo.CurrentlySelectedScript.dialogueScenes.Count- 1 < scriptInfo.CurrentDialogueScene )
               scriptInfo.CurrentDialogueScene = 0;

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
            if(scriptInfo.CurrentlySelectedScript.dialogueScenes.Count-1 > scriptInfo.CurrentDialogueScene)
                scriptInfo.CurrentDialogueScene++;
            else
            {
                //finished tutorial, return to main menu
            }
        }

        private void OnSkipClicked()
        {
            Debug.Log("clicked");
            displayDialogueSceneEvent.RaiseEvent(scriptInfo.CurrentDialogueScene);
        }
    }
}