using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
namespace InputSystem
{
    [CreateAssetMenu(fileName = "Gameplay Input Reader", menuName = "CarSimulator/ScriptableObjects/GameplayInputReader")]
    public class GameplayInputReader : ScriptableObject, InputActions.IGameplayActions
    {
        public bool IsGameplayInputBlocked { get; private set; }
        
        public event UnityAction<Vector2> SteerEvent = delegate { };
        public event UnityAction<Vector2> SteerCanceledEvent = delegate { };
        public bool SteerPressed { get; private set; }
        public event UnityAction StartEngineEvent = delegate { };
        public event UnityAction StartEngineCanceledEvent = delegate { };
        public bool StartEnginePressed { get; private set; }
        public event UnityAction BrakeEvent = delegate { };
        public event UnityAction BrakeCanceledEvent = delegate { };
        public bool BrakePressed { get; private set; }
        public event UnityAction GasEvent = delegate { };
        public event UnityAction GasCanceledEvent = delegate { };
        public bool GasPressed { get; private set; }
        public event UnityAction ShiftDownEvent = delegate { };
        public event UnityAction ShiftUpEvent = delegate { };
        public event UnityAction HandBrakeEvent = delegate { };
        public event UnityAction HandBrakeCanceledEvent = delegate { };
        public bool HandBrakePressed { get; private set; }
        public event UnityAction MenuEvent = delegate { };
        public event UnityAction ClutchEvent = delegate { };
        public event UnityAction ClutchCanceledEvent = delegate { };
        public bool ClutchPressed { get; private set; }
        private InputActions _inputActionsPlayer;
        private InputActions.IGameplayActions _gameplayActionsImplementation;

        //This have to be called once, before using the input system
        public void SetInput()
        {
            if (_inputActionsPlayer == null)
            {
                _inputActionsPlayer = new InputActions();

                _inputActionsPlayer.Gameplay.SetCallbacks(this);
            }
            _inputActionsPlayer.Gameplay.Enable();
        }

        // Enables/disables gameplay input. Disable gameplay input when player is in UI (MainMenu, Settings ...)
        public void GameplayInputEnabled(bool enabled)
        {
            if (enabled)
                _inputActionsPlayer.Gameplay.Enable();
            else
                _inputActionsPlayer.Gameplay.Disable();
            
            IsGameplayInputBlocked = enabled;
        }
        
        public void OnSteering(InputAction.CallbackContext context)
        {
            SteerEvent?.Invoke(context.ReadValue<Vector2>());

            SteerPressed = context.performed ? true : false;
        }

        public void OnStartEngine(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartEnginePressed= true;
                StartEngineEvent?.Invoke();
            }
            if (context.canceled)
            {
                StartEnginePressed = false;
                StartEngineCanceledEvent?.Invoke();
            }
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                BrakePressed= true;
                BrakeEvent?.Invoke();
            }
            if (context.canceled)
            {
                BrakePressed = false;
                BrakeCanceledEvent?.Invoke();
            }
        }

        public void OnGas(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                GasPressed= true;
                GasEvent?.Invoke();
            }
            if (context.canceled)
            {
                GasPressed = false;
                GasCanceledEvent?.Invoke();
            }
        }

        public void OnShiftDown(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ShiftDownEvent?.Invoke();
            }
        }

        public void OnShiftUp(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ShiftUpEvent?.Invoke();
            }
        }

        public void OnHandbrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                HandBrakePressed= true;
                HandBrakeEvent?.Invoke();
            }
            if (context.canceled)
            {
                HandBrakePressed = false;
                HandBrakeCanceledEvent?.Invoke();
            }
        }

      
        void InputActions.IGameplayActions.OnMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MenuEvent?.Invoke();
            }
        }

        public void OnClutch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ClutchPressed= true;
                ClutchEvent?.Invoke();
            }
            if (context.canceled)
            {
                ClutchPressed = false;
                ClutchCanceledEvent?.Invoke();
            }
        }

        public void ResetAllPressedFlags()
        {
            SteerPressed = false;
            StartEnginePressed = false;
            BrakePressed = false;
            GasPressed = false;
            HandBrakePressed = false;
            ClutchPressed = false;
        }
    }
}