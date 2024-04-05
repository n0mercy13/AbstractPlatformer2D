using System;
using UnityEngine;

namespace Codebase.Logic.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PickUpsHandler _pickUpsHandler;

        private int _coins;

        private void OnValidate()
        {
            if (_pickUpsHandler == null)
                throw new ArgumentNullException(nameof(_pickUpsHandler));
        }

        private void OnEnable()
        {
            _pickUpsHandler.CoinCollected += OnCoinCollected;
        }

        private void OnDisable()
        {
            _pickUpsHandler.CoinCollected -= OnCoinCollected;
        }

        private void OnCoinCollected(int value)
        {
            _coins += value;

#if UNITY_EDITOR
            Debug.Log($"Coins: {_coins})");
#endif
        }
    } 
}
