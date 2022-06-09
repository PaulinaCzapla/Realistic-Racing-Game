using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InputSystem
{
    [CreateAssetMenu(fileName = "UI Input Reader", menuName = "CarSimulator/ScriptableObjects/UIInputReader")]
    public class UIInputReader : ScriptableObject, InputActions.IUIActions
    {
        public bool IsUIInputBlocked { get; private set; }
        
        public event UnityAction<Vector2> NavigateEvent = delegate { };
        public event UnityAction SubmitEvent = delegate { };
        public event UnityAction CancelEvent = delegate { };
        public event UnityAction ClickEvent = delegate { };
        public event UnityAction MenuEvent = delegate { };
        public event UnityAction SkipDialogueEvent = delegate { };

        private InputActions _inputActionsPlayer;
        private InputActions.IUIActions _iuiActionsImplementation;
        
        //This have to be called at least once, before using input system
        public void SetInput()
        {
            if (_inputActionsPlayer == null)
            {
                _inputActionsPlayer = new InputActions();
                _inputActionsPlayer.UI.SetCallbacks(this);
            }
            _inputActionsPlayer.UI.Enable();
        }

        // Enables/disables ui input. Disable ui input when gameplay is active
        public void GameplayInputEnabled(bool enabled)
        {
            if (enabled)
                _inputActionsPlayer.UI.Enable();
            else
                _inputActionsPlayer.UI.Disable();
            
            IsUIInputBlocked = enabled;
        }
        
        public void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
                NavigateEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.performed)
                SubmitEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.performed)
                CancelEvent?.Invoke();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                ClickEvent?.Invoke();
        }

        public void OnPoint(InputAction.CallbackContext context) { }

        public void OnMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
                MenuEvent?.Invoke();
        }

        public void OnSkipDialogue(InputAction.CallbackContext context)
        {
            if (context.performed)
                SkipDialogueEvent?.Invoke();
        }
    }
}