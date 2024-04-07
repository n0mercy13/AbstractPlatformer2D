using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public class Bootstrap : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IAudioService _audioService;

        public Bootstrap(IGameFactory gameFactory, IAudioService audioService)
        {
            _gameFactory = gameFactory;
            _audioService = audioService;
        }

        public void Start()
        {
            InstantiateWorld();
            PlayBackgroundMusic();
        }

        private void PlayBackgroundMusic() => _audioService.PlayMusic();

        private void InstantiateWorld()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateEnemies();
            _gameFactory.CreateCoins();
            _gameFactory.CreateMedicalKits();
        }
    }
}