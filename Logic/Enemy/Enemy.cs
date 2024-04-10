using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic.EnemyComponents
{
    public partial class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAI _ai;
        [SerializeField] private HealthBarHandler _healthBarHandler;
        
        private IHealth _health;

        [Inject]
        private void Construct(IHealth health, EnemyConfig config)
        {
            _health = health;
            _health.Initialize(config.MaxHealth);

            _health.Changed += OnHealthChanged;
            _health.Death += OnDeath;
        }

        private void OnValidate()
        {
            if(_ai == null)
                throw new ArgumentNullException(nameof(_ai));
        }

        private void Start()
        {
            _health.ApplyDamage(0);
        }

        private void OnDisable()
        {
            _health.Changed -= OnHealthChanged;
            _health.Death -= OnDeath;
        }

        private void OnHealthChanged(int health, int maxHealth)
        {
            _healthBarHandler.Refresh(health, maxHealth);
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
