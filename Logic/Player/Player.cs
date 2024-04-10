using System;
using UnityEngine;
using VContainer;
using Codebase.StaticData;

namespace Codebase.Logic.PlayerComponents
{
    public partial class Player : Actor
    {
        [SerializeField] private PickUpsHandler _pickUpsHandler;

        private int _coins;

        [Inject]
        private void Construct(PlayerConfig config)
        {
            Health.Initialize(config.MaxHealth);
        }

        private void OnValidate()
        {
            if (_pickUpsHandler == null)
                throw new ArgumentNullException(nameof(_pickUpsHandler));
        }

        private void OnEnable()
        {
            _pickUpsHandler.CoinCollected += OnCoinCollected;
            _pickUpsHandler.MedicalKitCollected += OnMedicalKitCollected;
        }

        private void OnDisable()
        {
            _pickUpsHandler.CoinCollected -= OnCoinCollected;
            _pickUpsHandler.MedicalKitCollected -= OnMedicalKitCollected;
        }

        private void OnCoinCollected(int value)
        {
            _coins += value;

#if UNITY_EDITOR
            Debug.Log($"Coins: {_coins}");
#endif
        }

        private void OnMedicalKitCollected(int amount) => Health.Increase(amount);
    }

}
