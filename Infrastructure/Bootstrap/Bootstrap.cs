using System;
using VContainer.Unity;
using Codebase.Logic.EnemyComponents;
using Codebase.Logic.PlayerComponents;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public class Bootstrap : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IAudioService _audioService;
        private readonly UIManager _uiManager;
        private readonly HealthViewManager _healthViewManager;
        private readonly SceneData _sceneData;

        public Bootstrap(
            IGameFactory gameFactory, 
            IAudioService audioService, 
            UIManager uiManager, 
            HealthViewManager healthViewManager, 
            SceneData enemyConfig)
        {
            _gameFactory = gameFactory;
            _audioService = audioService;
            _uiManager = uiManager;
            _healthViewManager = healthViewManager;
            _sceneData = enemyConfig;
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
            CreatePlayer();
            CreateEnemies();
            _gameFactory.CreateCoins();
            _gameFactory.CreateMedicalKits();
        }

        private void CreateEnemies()
        {
            Enemy enemy;

            foreach (EnemyMarker marker in _sceneData.EnemyMarkers)
            {
                enemy = _gameFactory.CreateEnemy(marker.transform.position);
                enemy.Initialize(marker.PatrolRoute);
                _healthViewManager.Register(enemy);
            }
        }

        private void CreatePlayer()
        {
            Player player = _gameFactory.CreatePlayer();
            _healthViewManager.Register(player);
        }
    }
}