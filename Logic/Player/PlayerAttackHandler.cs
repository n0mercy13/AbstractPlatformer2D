using System;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;
using System.Collections;
using Codebase.Infrastructure.Services;
using System.Collections.Generic;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerAttackHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private IGameplayInput _input;
        private IAudioService _audioService;
        private IRaycastService _raycastService;
        private YieldInstruction _attackDelay;
        private Coroutine _attackRechargeCoroutine;
        private List<ITarget> _targets;
        private float _attackRadius;
        private int _damage;
        private bool _canAttack;

        [Inject]
        private void Construct(
            IGameplayInput input, 
            IAudioService audioService, 
            IRaycastService raycastService, 
            PlayerConfig config)
        {
            _input = input;
            _audioService = audioService;
            _raycastService = raycastService;
            _attackDelay = new WaitForSeconds(config.AttackSpeed);
            _targets = new List<ITarget>();
            _attackRadius = config.AttackRadius;
            _damage = config.Damage;
            _canAttack = true;

            _input.AttackPressed += OnAttackPressed;
        }

        private void OnValidate()
        {
            if(_player == null)
                throw new ArgumentNullException(nameof(_player));
        }

        private void OnDisable()
        {
            if (_attackRechargeCoroutine != null)
                StopCoroutine(_attackRechargeCoroutine);

            _input.AttackPressed -= OnAttackPressed;
        }

        private void OnAttackPressed()
        {
            if(_canAttack && _raycastService.IsTargetHit(_player, _attackRadius, out _targets))
            {
                _canAttack = false;
                ApplyDamage(_targets);
                _audioService.PlaySFX(AudioElementTypes.SFX_Player_Attack);
                _attackRechargeCoroutine = StartCoroutine(AttackRechargeAsync());
            }
            else
            {
                _audioService.PlaySFX(AudioElementTypes.SFX_Player_AttackNotReady);
            }
        }

        private void ApplyDamage(List<ITarget> targets)
        {
            foreach(ITarget target in targets)
                if(target is IDamageable damageable)
                    damageable.ApplyDamage(_damage);
        }

        private IEnumerator AttackRechargeAsync()
        {
            yield return _attackDelay;

            _canAttack = true;
        }
    }
}