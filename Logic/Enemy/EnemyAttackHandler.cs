﻿using Codebase.Infrastructure;
using Codebase.StaticData;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Codebase.Logic.EnemyComponents
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        private readonly int _capacity = 12;
        private IAudioService _audioService;
        private Coroutine _attackRechargeCoroutine;
        private Coroutine _attackCoroutine;
        private YieldInstruction _attackDelay;
        private Collider[] _hits;
        private float _attackRadius;
        private int _damage;
        private bool _canAttack;

        [Inject]
        private void Construct(IAudioService audioService, EnemyConfig config)
        {
            _audioService = audioService;
            _attackRadius = config.AttackRadius;
            _damage = config.Damage;
            _attackDelay = new WaitForSeconds(config.AttackSpeed);
            _hits = new Collider[_capacity];
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
            int hitsNumber = 0;

            while (enabled)
            {
                yield return new WaitUntil(() => _canAttack);

                hitsNumber = Physics.OverlapSphereNonAlloc(
                    transform.position, _attackRadius, _hits, _layerMask);

                for(int i = 0; i < hitsNumber; i++)
                {
                    if (_hits[i].TryGetComponent(out IDamageable damageable))
                    {
                        damageable.ApplyDamage(_damage);
                    }
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
