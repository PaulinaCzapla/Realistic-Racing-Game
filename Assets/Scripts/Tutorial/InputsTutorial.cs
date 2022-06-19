using System;
using System.Collections;
using Events.ScriptableObjects;
using InputSystem;
using UnityEngine;
using VisualNovel;
using VisualNovel.UI;

namespace Tutorial
{
    public class InputsTutorial : MonoBehaviour
    {
        [SerializeField] private UIInputReader input;
        [SerializeField] private GameplayInputReader inputGameplay;
        [SerializeField] private CarSO car;
        
        [Header("Events")]
        [SerializeField] private ScriptEventChannelSO displayDialoguePanelEvent;
        [SerializeField] private IntEventChannelSO displayDialogueSceneEvent;
        [SerializeField] private VoidEventChannelSO onReturnToMenu;
        [SerializeField] private VoidEventChannelSO onDialogueFinishedEvent;

        private int _dialogue = -1;
        private bool _wasSpeedig = false;
        private bool _didShiftUp;
        private bool _didShiftDown;
        private float _blockInputTime = 0;
        private float _holdTime = 0;
        private bool _started;
        private ScriptInfoSO _scriptInfo;
        
        private void OnDisable()
        {
            input.SkipDialogueEvent -=  OnSkipClicked;
        }

        private void OnSkipClicked()
        {
            if (_dialogue == 7 || _dialogue==2 || _dialogue == 0)
            {
                _dialogue++;
                displayDialogueSceneEvent.RaiseEvent(3);
                _blockInputTime = 0;
            }

            if (_dialogue == 9)
            {
                input.SkipDialogueEvent -=  OnSkipClicked;
                _scriptInfo.CurrentDialogueScene++;
            }
        }

        public void StartTutorial(ScriptInfoSO script)
        {
            input.GameplayInputEnabled(false);
            input.SkipDialogueEvent += OnSkipClicked;
            _scriptInfo = script;
           // displayDialoguePanelEvent.RaiseEvent(script, 3);
            displayDialogueSceneEvent.RaiseEvent(3);
            StartCoroutine(SetIndex());
        }

        IEnumerator SetIndex()
        {
           
            yield return new WaitForSeconds(3f);
            _dialogue = 0;
            input.GameplayInputEnabled(true); 
        }
        
        private void Update()
        {
            Debug.Log(_dialogue);
            switch (_dialogue)
            {
                case 1:
                {
                    _blockInputTime += Time.deltaTime;
                    
                    if(_blockInputTime > 2f)
                        inputGameplay.GameplayInputEnabled(true); 
                    
                    if (car.carSpeed >= 25)
                    {
                        _dialogue++;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                        input.GameplayInputEnabled(false);
                        _blockInputTime = 0;
                    }

                    break;
                }
                case 2:
                {
                    _blockInputTime += Time.deltaTime;

                    if (_blockInputTime > 2f)
                    {
                        input.GameplayInputEnabled(true);
                    }

                    break;
                }
                case 3:
                {
                    _blockInputTime += Time.deltaTime;
                    
                    if(_blockInputTime > 2f)
                        inputGameplay.GameplayInputEnabled(true);

                    if ((car.currentSteerAngle > 20 ||  car.currentSteerAngle < -20) && car.carSpeed >= 7)
                    {
                        _dialogue++;
                        _holdTime = 0;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                        _blockInputTime = 0;
                    }
                    break;
                }
                case 4:
                {
                    _blockInputTime += Time.deltaTime;
                    
                    if(_blockInputTime > 2f)
                        inputGameplay.GameplayInputEnabled(true);
                    
                    if (car.carSpeed >= 4 && inputGameplay.HandBrakePressed)
                        _wasSpeedig = true;

                    if (_wasSpeedig && car.carSpeed < 1 && inputGameplay.HandBrakePressed)
                    {
                        _dialogue++;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                        _blockInputTime = 0;
                    }
                    break;
                }
                case 5:
                {
                    _blockInputTime += Time.deltaTime;
                    
                    if(_blockInputTime > 3f)
                        inputGameplay.GameplayInputEnabled(true);
                    
                    if (inputGameplay.ClutchPressed && inputGameplay.ShiftUpGuard)
                        _didShiftUp = true;
                    if (inputGameplay.ClutchPressed && inputGameplay.ShiftDownGuard)
                        _didShiftDown = true;

                    if (_didShiftUp && _didShiftDown)
                    {
                        _dialogue++;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                        _blockInputTime = 0;
                    }
                    
                    break;
                }
                case 6:
                {
                    if(_blockInputTime > 2f)
                        inputGameplay.GameplayInputEnabled(true);
                    
                    if (inputGameplay.ReversePressed)
                        _holdTime += Time.deltaTime;

                    if (_holdTime > 1.5f)
                    {
                        _blockInputTime = 0;
                        _holdTime = 0;
                        _dialogue++;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                        input.GameplayInputEnabled(false);
                    }
                    break;
                }
                case 7:
                {
                    _blockInputTime += Time.deltaTime;

                    if (_blockInputTime > 2f)
                    {
                        input.GameplayInputEnabled(true);
                    }

                    break;
                }
                case 8:
                {
                    if(_blockInputTime > 2f)
                        inputGameplay.GameplayInputEnabled(true);
                    
                    if (inputGameplay.ReversePressed)
                        _holdTime += Time.deltaTime;

                    if (_holdTime > 1.5f)
                    {
                        _blockInputTime = 0;
                        _holdTime = 0;
                        _dialogue++;
                        displayDialogueSceneEvent.RaiseEvent(3);
                        inputGameplay.GameplayInputEnabled(false);
                    }
                    break;

                }
                case 9:
                {
                   
                        break;
                }
            }
        }
    }
}