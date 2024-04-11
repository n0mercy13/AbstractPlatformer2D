using System;
using System.Collections;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;
using System.Collections.Generic;
using Codebase.Infrastructure.Services;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private IAudioService _audioService;
        private IRaycastService _raycastService;
        private Coroutine _attackRechargeCoroutine;
        private Coroutine _attackCoroutine;
        private YieldInstruction _attackDelay;
        private List<ITarget> _targets;
        private float _attackRadius;
        private int _damage;
        private bool _canAttack;

        [Inject]
        private void Construct(IAudioService audioService, IRaycastService raycastService, EnemyConfig config)
        {
            _audioService = audioService;
            _raycastService = raycastService;
            _attackRadius = config.AttackRadius;
            _damage = config.Damage;
            _attackDelay = new WaitForSeconds(config.AttackSpeed);
            _targets = new List<ITarget>();
        }

        private void OnValidate()
        {
            if(_enemy == null)
                throw new ArgumentNullException(nameof(_enemy));
        }

        private void OnDisable()
        {
            StopAllAttackCoroutines();
        }

        public void AttackAsync()
        {
            _canAttack = true;

            StopAllAttackCoroutines();
            _attackCoroutine = StartCoroutine(Attack());
        }

        public void StopAttack() => StopAllAttackCoroutines();

        private IEnumerator Attack()
        {
            while (enabled)
            {
                yield return new WaitUntil(() => _canAttack);

                if(_raycastService.IsTargetHit(_enemy, _attackRadius, out _targets))
                {
                    foreach (ITarget target in _targets)
                        if(target is IDamageable damageable)
                            damageable.ApplyDamage(_damage);
                }

                _audioService.PlaySFX(AudioElementTypes.SFX_Enemy_Attack);
                _canAttack = false;
                _attackRechargeCoroutine = StartCoroutine(AttackRecharge());
            }
        }

        private IEnumerator AttackRecharge()
        {
            yield return _attackDelay;

            _canAttack = true;
        }

        private void StopAllAttackCoroutines()
        {
            if (_attackRechargeCoroutine != null)
                StopCoroutine(_attackRechargeCoroutine);

            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
        }
    }
}
