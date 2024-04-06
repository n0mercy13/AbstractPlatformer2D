using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    public partial class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAI _ai;
        
        private IHealth _health;

        [Inject]
        private void Construct(IHealth health, EnemyConfig config)
        {
            _health = health;
            _health.Initialize(config.MaxHealth);

            _health.HealthChanged += OnHealthChanged;
            _health.Death += OnDeath;
        }

        private void OnValidate()
        {
            if(_ai == null)
                throw new ArgumentNullException(nameof(_ai));
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
            _health.Death -= OnDeath;
        }

        private void OnHealthChanged(int health, int maxHealth)
        {
#if UNITY_EDITOR
            Debug.Log($"Enemy({gameObject.name}) health: {health} / {maxHealth}");
#endif
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }

    public partial class Enemy : IInitializable<Transform[]>
    {
        public void Initialize(Transform[] patrolRoute)
        {
            _ai.SetPatrolRoute(patrolRoute);
        }
    }

    public partial class Enemy : IDamageable
    {
        public void ApplyDamage(int amount) => _health.ApplyDamage(amount);
    }
}
