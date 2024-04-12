namespace Codebase.Logic
{
    public interface IHealable : ITarget
    {
        void HealSelf(int amount);
    }
}