using System;
using UnityEngine;
using VContainer;
using Codebase.Infrastructure;
using Codebase.StaticData;
using System.Collections;

namespace Codebase.Logic.PlayerComponents
{
    public class PlayerAttackHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        private IGameplayInput _input;
        private YieldInstruction _attackDelay;
        private Coroutine _attackRechargeCoroutine;
        private Collider[] _colliders;
        private float _attackRadius;
        private int _damage;
        private bool _canAttack;

        [Inject]
        private void Construct(IGameplayInput input, PlayerConfig config)
        {
            _input = input;
            _attackDelay = new WaitForSeconds(config.AttackSpeed);
            _attackRadius = config.AttackRadius;
            _damage = config.Damage;
            _canAttack = true;

            _input.AttackPressed += OnAttackPressed;
        }

        private void OnDisable()
        {
            if (_attackRechargeCoroutine != null)
                StopCoroutine(_attackRechargeCoroutine);

            _input.AttackPressed -= OnAttackPressed;
        }

        private void OnAttackPressed()
        {
            if(_canAttack == false)
                return;

            _canAttack = false;
            ApplyDamage();
            StartCoroutine(RechargeAttackAsync());
        }

        private void ApplyDamage()
        {
            _colliders = Physics.OverlapSphere(
                transform.position, _attackRadius, _layerMask);

            foreach(Collider collider in _colliders)
            {
                if(collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                }
            }
        }

        private IEnumerator RechargeAttackAsync()
        {
            yield return _attackDelay;

            _canAttack = true;
        }
    }
}