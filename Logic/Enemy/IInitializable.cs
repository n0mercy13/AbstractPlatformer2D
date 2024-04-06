namespace Codebase.Logic.Enemy
{
    public interface IInitializable<T> where T : class
    {
        void Initialize(T instance);
    }
}