namespace Codebase.Logic
{
    public interface IInitializable
    {
        void Initialize();
    }

    public interface IInitializable<T> where T : class
    {
        void Initialize(T instance);
    }
}