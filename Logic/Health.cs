using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Codebase.Logic
{
    public class Health : IHealth
    {
        private int _value;
        private int _maxValue;

        public event Action<int, int> Changed;
        public event Action Depleted;
        
        public void Initialize(int maxHealth)
        {
            _maxValue = maxHealth;
            _value = _maxValue;
        }

        public void Decrease(int amount)
        {
            Assert.IsTrue(amount >= 0);

            _value -= amount;
            _value = Mathf.Max(_value, 0);
            Changed?.Invoke(_value, _maxValue);

            if (_value == 0)
                Depleted?.Invoke();
        }

        public void Increase(int amount)
        {
            Assert.IsTrue(amount >= 0);

            _value += amount;
            _value = Mathf.Min(_value, _maxValue);
            Changed?.Invoke(_value, _maxValue);
        }
    }
}
