using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public class Bootstrap : IStartable
    {
        private readonly IGameFactory _gameFactory;

        public Bootstrap(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Start()
        {
            InstantiateWorld();
        }

        private void InstantiateWorld()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateCoins();
            _gameFactory.CreateMedicalKits();
        }
    }
}