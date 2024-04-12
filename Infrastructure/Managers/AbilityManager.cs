using System;
using Codebase.Logic;
using Codebase.Logic.PlayerComponents;
using System.Collections.Generic;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public partial class PlayerAbilityManager
    {
        private readonly IGameplayInput _gameplayInput;
        private readonly IRaycastService _raycastService;
        private readonly Vampirisms _vampirisms;
        private readonly float _abilityRange = 3.0f;
        private Player _player;
        private bool _canUseAbility;

        public PlayerAbilityManager(IGameplayInput gameplayInput, IRaycastService raycastService, Vampirisms vampirisms)
        {
            _gameplayInput = gameplayInput;
            _raycastService = raycastService;
            _vampirisms = vampirisms;
            _canUseAbility = true;

            _gameplayInput.UseAbilityPressed += OnUseAbilityPressed;
        }

        private void OnUseAbilityPressed()
        {
            if (_canUseAbility == false
                || _raycastService.IsTargetHit(_player, _abilityRange, out List<ITarget> targets) == false)
                    return;

            _canUseAbility = false;
            ITarget target = targets[0];
            _vampirisms.Use(_player, target, OnAbilityPerformed);
        }

        private void OnAbilityPerformed() => _canUseAbility = true;
    }

    public partial class PlayerAbilityManager : IInitializable<Player>
    {
        public void Initialize(Player player)
        {
            _player = player;
        }
    }

    public partial class PlayerAbilityManager : IDisposable
    {
        public void Dispose()
        {
            _gameplayInput.UseAbilityPressed -= OnUseAbilityPressed;
        }
    }
}
