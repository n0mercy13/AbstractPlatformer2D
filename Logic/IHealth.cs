using System;

namespace Codebase.Logic
{
    public interface IHealth
    {
        event Action<int, int> Changed;
        event Action Depleted;

        void Initialize(int maxHealth);
        void Decrease(int amount);
        void Increase(int amount);
    }
}