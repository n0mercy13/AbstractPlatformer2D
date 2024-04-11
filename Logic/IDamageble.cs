namespace Codebase.Logic
{
    public interface IDamageable : ITarget
    {
        void ApplyDamage(int amount);
    }
}
