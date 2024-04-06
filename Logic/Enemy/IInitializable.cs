namespace Codebase.Logic.EnemyComponents
{
    public interface IInitializable<T> where T : class
    {
        void Initialize(T instance);
    }
}