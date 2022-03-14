using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
namespace InputSystem
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "CarSimulator/ScriptableObjects/InputReader")]
    public class InputReader : ScriptableObject, InputActions.IGameplayActions, InputActions.IUIActions
    {
        public bool IsGameplayInputBlocked { get; set; }
        
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public bool MovePressed { get; set; }
        
        public event UnityAction StartEngineEvent = delegate { };
        public bool StartEnginePressed { get; private set; }
        
        private InputActions _inputActionsPlayer;
        private InputActions.IGameplayActions _gameplayActionsImplementation;
        private InputActions.IUIActions _iuiActionsImplementation;
        
        public void SetInput()
        {
            if (_inputActionsPlayer == null)
            {
                _inputActionsPlayer = new InputActions();

                _inputActionsPlayer.Gameplay.SetCallbacks(this);
                _inputActionsPlayer.UI.SetCallbacks(this);
            }
            _inputActionsPlayer.Gameplay.Enable();
            _inputActionsPlayer.UI.Enable();
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
        
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
            MovePressed = context.ReadValue<Vector2>().x != 0 ? true : false;
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
                StartEngineEvent?.Invoke();
            }
        }
    }
}