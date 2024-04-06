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

        private readonly int _simultaneousHits = 10;
        private IGameplayInput _input;
        private YieldInstruction _attackDelay;
        private Coroutine _attackRechargeCoroutine;
        private Collider[] _hits;
        private float _attackRadius;
        private int _damage;
        private bool _canAttack;

        [Inject]
        private void Construct(IGameplayInput input, PlayerConfig config)
        {
            _input = input;
            _attackDelay = new WaitForSeconds(config.AttackSpeed);
            _hits = new Collider[_simultaneousHits];
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
            _attackRechargeCoroutine = StartCoroutine(AttackRechargeAsync());
        }

        private void ApplyDamage()
        {
            int numberHits = Physics.OverlapSphereNonAlloc(
                transform.position, _attackRadius, _hits, _layerMask);

            for(int i = 0; i < numberHits; i++)
            {
                if (_hits[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                }
            }
        }

        private IEnumerator AttackRechargeAsync()
        {
            yield return _attackDelay;

            _canAttack = true;
        }
    }
}