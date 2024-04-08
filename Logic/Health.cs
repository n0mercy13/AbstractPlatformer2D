using System;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace Codebase.Logic
{
    public partial class Health
    {
        private int _value;
        private int _maxValue;
    }

    public partial class Health : IHealth
    {
        public event Action<int, int> HealthChanged = delegate { };
        public event Action Death = delegate { };
        
        public void Initialize(int maxHealth)
        {
            _maxValue = maxHealth;
            _value = _maxValue;
        }

        public void ApplyDamage(int amount)
        {
            Assert.IsTrue(amount >= 0);

            _value -= amount;
            _value = Mathf.Max(_value, 0);
            HealthChanged.Invoke(_value, _maxValue);

            if (_value == 0)
                Death.Invoke();
        }

        public void Heal(int amount)
        {
            Assert.IsTrue(amount >= 0);

            _value += amount;
            _value = Mathf.Min(_value, _maxValue);
            HealthChanged.Invoke(_value, _maxValue);
        }
    }
}
