using System;
using System.Collections;
using UnityEngine;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Logic
{
    public partial class Vampirisms
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly int _abilityStrength = 2;
        private readonly float _frequency = 0.5f;
        private readonly float _duration = 6.0f;

        private bool _isActive;
        private Coroutine _damageCoroutine;
        private Coroutine _healCoroutine;
        private Coroutine _deactivationCoroutine;
        private YieldInstruction _durationDelay;
        private YieldInstruction _abilityDelay;

        public Vampirisms(SceneData data)
        {
            _coroutineRunner = data.CoroutineRunner;
            _durationDelay = new WaitForSeconds(_duration);
            _abilityDelay = new WaitForSeconds(_frequency);
        }

        public void Use(ITarget initiator, ITarget target, Action onCompletion = null)
        {
            if (_isActive)
                return;

            _isActive = true;
            StopAllAbilityCoroutines();
            _deactivationCoroutine = _coroutineRunner
                .StartCoroutine(DeactivationAsync(onCompletion));
            _damageCoroutine = _coroutineRunner
                .StartCoroutine(DamageAsync(target));
            _healCoroutine = _coroutineRunner
                .StartCoroutine(HealAsync(initiator));
        }

        private IEnumerator HealAsync(ITarget initiator)
        {
            if(initiator is IHealable healable == false)
                throw new InvalidOperationException(nameof(initiator));

            while(healable != null && _isActive)
            {
                healable.HealSelf(_abilityStrength);

                yield return _abilityDelay;
            }

            _isActive = false;
        }

        private IEnumerator DamageAsync(ITarget target)
        {
            if(target is IDamageable damageable == false)
                throw new InvalidOperationException(nameof(target));

            while(damageable != null && _isActive)
            {
                damageable.ApplyDamage(_abilityStrength);

                yield return _abilityDelay;
            }
            
            _isActive = false;
        }

        private IEnumerator DeactivationAsync(Action onCompletion)
        {
            yield return _durationDelay;

            _isActive = false;

            if(onCompletion != null)
                onCompletion?.Invoke();
        }

        private void StopAllAbilityCoroutines()
        {
            if (_damageCoroutine != null)
                _coroutineRunner.StopCoroutine(_damageCoroutine);

            if (_healCoroutine != null)
                _coroutineRunner.StopCoroutine(_healCoroutine);

            if (_deactivationCoroutine != null)
                _coroutineRunner.StopCoroutine(_deactivationCoroutine);
        }
    }

    public partial class Vampirisms : IDisposable
    {
        public void Dispose()
        {
            StopAllAbilityCoroutines();
        }
    }
}
