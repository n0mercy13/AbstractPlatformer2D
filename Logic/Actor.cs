using System;
using UnityEngine;
using VContainer;

namespace Codebase.Logic
{
    public partial class Actor : MonoBehaviour
    {
        [SerializeField] private Transform _healthBarMarker;
        private IHealth _health = new Health();

        private void OnValidate()
        {
            if (_healthBarMarker == null)
                throw new ArgumentNullException(nameof(_healthBarMarker));
        }

        private void OnEnable()
        {
            _health.Changed += OnHealthChanged;
            _health.Depleted += OnHealthDepleted;
        }

        private void OnDisable()
        {
            _health.Changed -= OnHealthChanged;
            _health.Depleted -= OnHealthDepleted;
        }

        public event Action<int, int> HealthChanged;
        public event Action<Actor> Death;

        public Vector3 HealthBarPosition => _healthBarMarker.position;
        protected IHealth Health => _health;

        private void OnHealthChanged(int health, int maxHealth) => 
            HealthChanged?.Invoke(health, maxHealth);

        private void OnHealthDepleted()
        {
            Death?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public partial class Actor : IDamageable
    {
        public void ApplyDamage(int amount) => _health.Decrease(amount);
    }
}
