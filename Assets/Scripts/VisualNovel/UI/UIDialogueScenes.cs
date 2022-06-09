using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VisualNovel.Dialogues;

namespace VisualNovel.UI
{
    public class UIDialogueScenes : MonoBehaviour
    {
         [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip lettersClip;
        
        [Header("Scriptable objects")]
        [SerializeField] private ScriptSO script;
        [SerializeField] private ScriptInfoSO scriptInfo;
        
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private GameObject panel;
        [SerializeField] private SkipArrow arrow;
        [SerializeField] private Image[] charactersImages;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;
        
        [Header("Fade")]
        [SerializeField] private List<Fade> componentsToFade;
        
        private const float InitialPauseBetweenLetters = 0.015f;
        private const float InitialPauseBetweenSounds = 0.04f;
        private const float ShortPauseBetweenLetters = 0.001f;
        private const float CloseAfter = 1.5f;
        
        private float _pauseBetweenLetters;
        private bool _isSegmentDisplayedCurrently;
        private int _dialogue = 0;
        private bool _canDisplay = false;
        private bool _isDialogueActive;
        private bool _wasInputDisabled;
        private float _timeElapsed = 0;
        private int _currentScene = 0;
        private bool _isFading = false;
        
        private void OnEnable()
        {
            arrow.gameObject.SetActive(false);
            _pauseBetweenLetters = InitialPauseBetweenLetters;
            displayDialoguePanelEvent.OnEventRaised += DisplayDialoguePanel;
            displayDialogueSceneEvent.OnEventRaised += DisplayDialogueSceneSegment;
            _timeElapsed = 0;
        }

        private void OnDisable()
        {
            displayDialoguePanelEvent.OnEventRaised -= DisplayDialoguePanel;
            displayDialogueSceneEvent.OnEventRaised -= DisplayDialogueSceneSegment;

            StopAllCoroutines();

           // if (_isDialogueActive)
             //   CloseDialogue();
        }

        private void OnStartFadeIn(GameObject arg0)
        {
           // CloseDialogue();
        }
        // private void CloseDialogue()
        // {
        //     arrow.gameObject.SetActive(false);
        //     _canDisplay = false;
        //     _dialogue = 0;
        //     _isDialogueActive = false;
        //     onDialogueFinishedEvent.RaiseEvent();
        //     FadeOutPanel();
        //     _pauseBetweenLetters = InitialPauseBetweenLetters;
        // }

        private void Update()
        {
            _timeElapsed += Time.deltaTime;
        }

        private void DisplayDialogueSceneSegment(int sceneIndex)
        {
            Debug.Log(sceneIndex);
            if (_canDisplay)
            {
                if (_currentScene != sceneIndex)
                {
                    _currentScene = sceneIndex;
                    _dialogue = 0;
                }

                if (!_isSegmentDisplayedCurrently)
                {
                    _pauseBetweenLetters = InitialPauseBetweenLetters;
                    
                    if (_dialogue < script.dialogueScenes[sceneIndex].dialogues.Count)
                    {
                        text.text = String.Empty;
                        name.text = script.dialogueScenes[sceneIndex].dialogues[_dialogue].name;
                        
                        _pauseBetweenLetters = InitialPauseBetweenLetters;

                        SetCharactersOnScene(script.dialogueScenes[sceneIndex].dialogues[_dialogue]);
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

        private void SetCharactersOnScene(DialogueSO dialogue)
        {
            int images = Mathf.Clamp(dialogue.sceneElements.Count, 0, charactersImages.Length);

            if (dialogue.shouldOverridePreviousDialogue)
            {
                foreach (var image in charactersImages)
                {
                    image.sprite = null;
                    Color color;
                    ColorUtility.TryParseHtmlString("#9D9D9D", out color);
                    image.color = color;
                    image.enabled = false;
                }
            }

            int numOfFadedIn = 0;
            
            for (int i = 0; i < images; i++)
            {
                Color color; 
                
                if (i == 0)
                    ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                else
                    ColorUtility.TryParseHtmlString("#9D9D9D", out color);
                
                foreach (var image in charactersImages)
                {
                    if (!image.enabled)
                    {
                        image.color = color;
                        image.sprite = dialogue.sceneElements[i].sprite;
                        image.transform.localPosition = dialogue.sceneElements[i].position;

                        if (dialogue.shouldFadeIn && numOfFadedIn < dialogue.numOfFadeInCharacters)
                        {
                            image.color = new Color(image.color.r, image.color.g, image.color.b,
                                0);
                            if (i == images - 1)
                            {
                                _isFading = true;
                                image.GetComponent<Fade>().OnFadedIn += () => _isFading = false;
                            }

                            image.GetComponent<Fade>().FadeIn();
                            numOfFadedIn++;
                        }

                        image.enabled = true;
                        break;
                    }
                }
            }
        }
        private void DisplayDialoguePanel(ScriptSO script, int firstScene)
        {
            _pauseBetweenLetters = InitialPauseBetweenLetters;

            //if (_isDialogueActive)
              //  CloseDialogue();

            _currentScene = firstScene;
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
                yield return new WaitUntil(() => _isFading == false);
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
            DisplayDialogueSceneSegment(_currentScene);
        }
    }
}