using System;

namespace Codebase.Infrastructure
{
    public partial class AbilityManager
    {
        private readonly IGameplayInput _gameplayInput;
        private bool _canUseAbility;

        public AbilityManager(IGameplayInput gameplayInput)
        {
            _gameplayInput = gameplayInput;

            _gameplayInput.UseAbilityPressed += OnUseAbilityPressed;
        }

        private void OnUseAbilityPressed()
        {
            if (_canUseAbility == false)
                return;

            _canUseAbility = false;

        }
    }

    public partial class AbilityManager : IDisposable
    {
        public void Dispose()
        {
            _gameplayInput.UseAbilityPressed -= OnUseAbilityPressed;
        }
    }
}
