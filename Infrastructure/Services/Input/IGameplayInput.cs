using System;

namespace Codebase.Infrastructure
{
    public interface IGameplayInput
    {
        event Action<float> HorizontalDirectionChanged;
        event Action<bool> IsRunPressed;
        event Action JumpPressed;
        event Action AttackPressed;
        event Action UseAbilityPressed;
    }
}