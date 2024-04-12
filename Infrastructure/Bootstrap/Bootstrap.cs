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
        private readonly PlayerAbilityManager _playerAbilityManager;
        private readonly SceneData _sceneData;
        private Player _player;

        public Bootstrap(
            IGameFactory gameFactory,
            IAudioService audioService,
            UIManager uiManager,
            HealthViewManager healthViewManager,
            PlayerAbilityManager playerAbilityManager,
            SceneData enemyConfig)
        {
            _gameFactory = gameFactory;
            _audioService = audioService;
            _uiManager = uiManager;
            _healthViewManager = healthViewManager;
            _playerAbilityManager = playerAbilityManager;
            _sceneData = enemyConfig;
        }

        public void Start()
        {
            InstantiateWorld();
            InitializeManagers();
            PlayBackgroundMusic();
        }

        private void InitializeManagers()
        {
            _uiManager.Initialize();
            _playerAbilityManager.Initialize(_player);
            _healthViewManager.Register(_player);
        }

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
            _player = _gameFactory.CreatePlayer();
        }
    }
}