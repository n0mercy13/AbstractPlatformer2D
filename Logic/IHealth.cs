using System;

namespace Codebase.Logic
{
    public interface IHealth
    {
        event Action<int, int> HealthChanged;
        event Action Death;

        void Initialize(int maxHealth);
        void ApplyDamage(int amount);
        void Heal(int amount);
    }
}