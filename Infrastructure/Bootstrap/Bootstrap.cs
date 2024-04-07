using System;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public class Bootstrap : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IAudioService _audioService;
        private readonly UIManager _uiManager;

        public Bootstrap(IGameFactory gameFactory, IAudioService audioService, UIManager uiManager)
        {
            _gameFactory = gameFactory;
            _audioService = audioService;
            _uiManager = uiManager;
        }

        public void Start()
        {
            InitializeManagers();
            InstantiateWorld();
            PlayBackgroundMusic();
        }

        private void InitializeManagers() => _uiManager.Initialize();

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