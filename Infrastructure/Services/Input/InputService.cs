using System;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Codebase.Infrastructure
{
    public partial class InputService
    {
        private readonly InputActions _inputs;

        public InputService(InputActions inputs)
        {
            _inputs = inputs;
            _inputs.Enable();

            _inputs.Gameplay.HorizontalAxis.performed += OnHorizontalAxisPerformed;
            _inputs.Gameplay.HorizontalAxis.canceled += OnHorizontalAxisCanceled;
            _inputs.Gameplay.Run.performed += OnRunPerformed;
            _inputs.Gameplay.Run.canceled += OnRunCanceled;
            _inputs.Gameplay.Jump.performed += OnJumpPerformed;
            _inputs.Gameplay.Attack.performed += OnAttackPerformed;
            _inputs.Gameplay.UseAbility.performed += OnUseAbilityPerformed;
            _inputs.UI.OpenSettings.performed += OnOpenSettingsPerformed;
            _inputs.UI.Cancel.performed += OnCancelPerformed;
            _inputs.UI.Submit.performed += OnSubmitPerformed;
        }

        private void OnOpenSettingsPerformed(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
                OpenSettingsPressed.Invoke();
        }

        private void OnCancelPerformed(CallbackContext context)
        {
            if(context.phase.Equals(InputActionPhase.Performed))
                CanceledPressed.Invoke();
        }

        private void OnSubmitPerformed(CallbackContext context)
        {
            if(context.phase.Equals(InputActionPhase.Performed))
                SubmitPressed.Invoke();
        }

        private void OnHorizontalAxisPerformed(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
            {
                float direction = context.ReadValue<float>();
                HorizontalDirectionChanged.Invoke(direction);
            }
        }

        private void OnHorizontalAxisCanceled(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Canceled))
                HorizontalDirectionChanged.Invoke(0.0f);
        }

        private void OnRunPerformed(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
                IsRunPressed.Invoke(true);
        }

        private void OnRunCanceled(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Canceled))
                IsRunPressed.Invoke(false);
        }

        private void OnJumpPerformed(CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
                JumpPressed.Invoke();
        }

        private void OnAttackPerformed(CallbackContext context)
        {
            if(context.phase.Equals(InputActionPhase.Performed))
                AttackPressed.Invoke();
        }

        private void OnUseAbilityPerformed(CallbackContext context)
        {
            if(context.phase.Equals(InputActionPhase.Performed))
                UseAbilityPressed.Invoke();
        }
    }

    public partial class InputService : IGameplayInput
    {
        public event Action<float> HorizontalDirectionChanged = delegate { };
        public event Action<bool> IsRunPressed = delegate { };
        public event Action JumpPressed = delegate { };
        public event Action AttackPressed = delegate { };
        public event Action UseAbilityPressed;
    }

    public partial class InputService : IUIInput 
    { 
        public event Action OpenSettingsPressed = delegate { };
        public event Action CanceledPressed = delegate { };
        public event Action SubmitPressed = delegate { };
    }

    public partial class InputService : IDisposable
    {
        public void Dispose()
        {
            _inputs.Gameplay.HorizontalAxis.performed -= OnHorizontalAxisPerformed;
            _inputs.Gameplay.HorizontalAxis.canceled -= OnHorizontalAxisCanceled;
            _inputs.Gameplay.Run.performed -= OnRunPerformed;
            _inputs.Gameplay.Run.canceled -= OnRunCanceled;
            _inputs.Gameplay.Jump.performed -= OnJumpPerformed;
            _inputs.Gameplay.Attack.performed -= OnAttackPerformed;
            _inputs.Gameplay.UseAbility.performed -= OnUseAbilityPerformed;
            _inputs.UI.OpenSettings.performed -= OnOpenSettingsPerformed;
            _inputs.UI.Cancel.performed -= OnCancelPerformed;
            _inputs.UI.Submit.performed -= OnSubmitPerformed;
        }
    }
}
