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
            InstantiateActors();
        }

        private void InstantiateActors()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateCoins();
        }
    }
}