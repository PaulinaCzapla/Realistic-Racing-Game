using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using InputSystem;
using TMPro;
using UI;
using UnityEngine;
using VisualNovel.Dialogues;

namespace VisualNovel.UI
{
    public class UIDialogueScenes : MonoBehaviour
    {
         [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip lettersClip;
        
        [Header("Scriptable objects")] 
        [SerializeField] private UIInputReader input;
        [SerializeField] private ScriptSO script;
        
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private GameObject panel;
        [SerializeField] private SkipArrow arrow;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;
        
        [Header("Fade")]
        [SerializeField] private List<Fade> componentsToFade;
        
        
        private const float InitialPauseBetweenLetters = 0.04f;
        private const float InitialPauseBetweenSounds = 0.04f;
        private const float ShortPauseBetweenLetters = 0.005f;
        private const float CloseAfter = 1.5f;
        
        private float _pauseBetweenLetters;
        private bool _isSegmentDisplayedCurrently;
        private int _dialogue;
        private bool _canDisplay = false;
        private bool _isDialogueActive;
        private bool _wasInputDisabled;
        private float _timeElapsed = 0;
        private int _currentScene = 0;
        
        

        private void OnEnable()
        {
            input.SkipDialogueEvent += () => DisplayDialogueSceneSegment(_currentScene );
            arrow.gameObject.SetActive(false);
            _pauseBetweenLetters = InitialPauseBetweenLetters;
            displayDialoguePanelEvent.OnEventRaised += DisplayDialoguePanel;
            displayDialogueSceneEvent.OnEventRaised += DisplayDialogueSceneSegment;
            _timeElapsed = 0;
        }

        private void OnDisable()
        {
            input.SkipDialogueEvent -= () => DisplayDialogueSceneSegment(_currentScene );
            displayDialoguePanelEvent.OnEventRaised -= DisplayDialoguePanel;
            displayDialogueSceneEvent.OnEventRaised -= DisplayDialogueSceneSegment;

            StopAllCoroutines();

            if (_isDialogueActive)
                CloseDialogue();
        }

        private void OnStartFadeIn(GameObject arg0)
        {
            CloseDialogue();
        }
        private void CloseDialogue()
        {
            arrow.gameObject.SetActive(false);
            _canDisplay = false;
            _dialogue = 0;
            _isDialogueActive = false;
            onDialogueFinishedEvent.RaiseEvent();
            FadeOutPanel();
            _pauseBetweenLetters = InitialPauseBetweenLetters;
        }

        private void Update()
        {
            _timeElapsed += Time.deltaTime;
        }

        private void DisplayDialogueSceneSegment(int sceneIndex)
        {
            if (_canDisplay)
            {
                _currentScene = sceneIndex;
                if (!_isSegmentDisplayedCurrently)
                {
                    _pauseBetweenLetters = InitialPauseBetweenLetters;
                    
                    if (_dialogue < script.dialogueScenes[sceneIndex].dialogues.Count)
                    {
                        text.text = String.Empty;
                        name.text = script.dialogueScenes[sceneIndex].dialogues[_dialogue].name;
                        
                        _pauseBetweenLetters = InitialPauseBetweenLetters;
                        
                        StartCoroutine(DisplayDialoguePart(script.dialogueScenes[sceneIndex].dialogues[_dialogue].dialogueSegment));
                        _dialogue++;
                        
                    }
                    else
                    {
                        //StartCoroutine(CloseAutomatically());
                        //event - dialogue displayed, waiting for next
                        
                        onDialogueFinishedEvent.RaiseEvent();
                    }
                }
                else
                {
                    _pauseBetweenLetters = ShortPauseBetweenLetters;
                }
            }
        }

        private void DisplayDialoguePanel(ScriptSO script)
        {
            _pauseBetweenLetters = InitialPauseBetweenLetters;

            if (_isDialogueActive)
                CloseDialogue();

            text.text = String.Empty;
            _isDialogueActive = true;
            this.script = script;
            FadeInPanel();
            _isSegmentDisplayedCurrently = false;
            StartCoroutine(DisplayTextTimer());
            
                // input.GameplayInputEnabled(false);
                // _wasInputDisabled = true;
            
        }


        private IEnumerator DisplayDialoguePart(string dialogue)
        {
            arrow.gameObject.SetActive(false);

            if (dialogue != null)
            {
                _isSegmentDisplayedCurrently = true;
                int i = 0;
                _timeElapsed = 0;

                foreach (var ch in dialogue)
                {
                    text.text += ch;

                    if (InitialPauseBetweenSounds <= _timeElapsed)
                    {
                        _timeElapsed = 0;
                        //audioSource.PlayOneShot(audioSource.clip);
                    }
                    
                    yield return new WaitForSeconds(_pauseBetweenLetters);
                }

                _isSegmentDisplayedCurrently = false;
            }

            arrow.gameObject.SetActive(true);
            _pauseBetweenLetters = InitialPauseBetweenLetters;
        }

        private void FadeInPanel()
        {
            foreach (var fade in componentsToFade)
            {
                fade.FadeIn();
            }
        }
        
        private void FadeOutPanel()
        {
            foreach (var fade in componentsToFade)
            {
                fade.FadeOut();
            }
        }

        private IEnumerator DisplayTextTimer()
        {
            arrow.gameObject.SetActive(false);
            _canDisplay = false;
            yield return new WaitForSeconds(0.5f);
            _canDisplay = true;
            DisplayDialogueSceneSegment(0);
        }
        
    }
}