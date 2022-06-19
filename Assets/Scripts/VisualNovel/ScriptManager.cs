using System;
using System.Collections;
using CameraFollow;
using Events.ScriptableObjects;
using InputSystem;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualNovel
{
    public class ScriptManager : MonoBehaviour
    {
        [SerializeField] private UIInputReader input;
        [SerializeField] private GameplayInputReader inputGameplay;
        [SerializeField] private ScriptInfoSO scriptInfo;
        [SerializeField] private Camera camera;
        [SerializeField] private InputsTutorial inputsTutorial;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;
        
        private Vector3 pos1 = new Vector3(-18.8f, 2.3f, 146.3f);
        private Vector3 rot1 = new Vector3(1.3f, -310f, 0);
        private Vector3 pos2 = new Vector3(2.3f, 0.3f, 7.33f);
        private Vector3 rot2 = new Vector3(6.9f, -11.24f, 0);

        private IEnumerator Start()
       {
           input.SetInput();
           inputGameplay.SetInput();
           inputGameplay.GameplayInputEnabled(false);
           scriptInfo.CurrentDialogueScene = 2;
           if (scriptInfo.CurrentDialogueScene >= 2)
           {
               camera.transform.localPosition = pos2;
               camera.transform.rotation = Quaternion.Euler(rot2);
           }
           else
           {
               camera.transform.localPosition = pos1;
               camera.transform.rotation = Quaternion.Euler(rot1);
           }

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
                SceneManager.LoadSceneAsync("PersistentScene", LoadSceneMode.Additive);
                SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            }

            if (scriptInfo.CurrentDialogueScene == 2)
            {
                camera.transform.localPosition = pos2;
                camera.transform.rotation = Quaternion.Euler(rot2); 
                OnSkipClicked();
            }
            
            if (scriptInfo.CurrentDialogueScene == 3)
            {
                camera.GetComponent<FollowingCamera>().enabled = true;
                inputsTutorial.StartTutorial(scriptInfo);
                //OnSkipClicked();
            }
        }

        private void OnSkipClicked()
        {
            Debug.Log("clicked");
            if(scriptInfo.CurrentDialogueScene != 3)
                 displayDialogueSceneEvent.RaiseEvent(scriptInfo.CurrentDialogueScene);

        }
    }
}