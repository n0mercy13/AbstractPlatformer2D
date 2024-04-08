using Codebase.StaticData;
using System;
using UnityEngine;
using VContainer;

namespace Codebase.Logic.PlayerComponents
{
    public partial class Player : MonoBehaviour
    {
        [SerializeField] private PickUpsHandler _pickUpsHandler;
        [SerializeField] private HealthBarHandler _healthBarHandler;

        private IHealth _health;
        private int _coins;

        [Inject]
        private void Construct(PlayerConfig config, IHealth health)
        {
            _health = health;
            _health.Initialize(config.MaxHealth);
            _health.HealthChanged += OnHealthChanged;
            _health.Death += OnDeath;
        }

        private void OnValidate()
        {
            if (_pickUpsHandler == null)
                throw new ArgumentNullException(nameof(_pickUpsHandler));

            if(_healthBarHandler == null)
                throw new ArgumentNullException(nameof(_healthBarHandler));
        }

        private void OnEnable()
        {
            _pickUpsHandler.CoinCollected += OnCoinCollected;
            _pickUpsHandler.MedicalKitCollected += OnMedicalKitCollected;
        }

        private void Start()
        {
            _health.ApplyDamage(0);
        }

        private void OnDisable()
        {
            _health.HealthChanged -= OnHealthChanged;
            _health.Death -= OnDeath;
            _pickUpsHandler.CoinCollected -= OnCoinCollected;
        }

        private void OnHealthChanged(int health, int maxHealth) => 
            _healthBarHandler.UpdateView(health, maxHealth);

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnCoinCollected(int value)
        {
            _coins += value;

#if UNITY_EDITOR
            Debug.Log($"Coins: {_coins}");
#endif
        }

        private void OnMedicalKitCollected(int amount)
        {
            _health.Heal(amount);
        }
    }

    public partial class Player : IDamageable
    {
        public void ApplyDamage(int amount) => _health.ApplyDamage(amount);
    }
}
