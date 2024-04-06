namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void CreateEnemies();
        void CreateCoins();
        void CreateMedicalKits();
    }
}