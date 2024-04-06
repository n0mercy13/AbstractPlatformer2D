using Codebase.Logic;
using Codebase.Logic.EnemyComponents;
using Codebase.Logic.PlayerComponents;
using Codebase.StaticData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public partial class GameFactory
    {
        private readonly IObjectResolver _container;
        private readonly System.Random _random;
        private readonly int _seed = 1;
        private readonly SceneData _sceneData;
        private readonly Player _playerPrefab;
        private readonly Enemy _enemyPrefab;
        private readonly Coin _coinPrefab;
        private readonly MedicalKit _medicalKitPrefab;

        public GameFactory(
            IObjectResolver container,
            SceneData sceneData,
            PlayerConfig playerConfig,
            EnemyConfig enemyConfig,
            PickUpsConfig pickUpConfig)
        {
            _container = container;
            _sceneData = sceneData;
            _playerPrefab = playerConfig.Prefab;
            _enemyPrefab = enemyConfig.Prefab;
            _coinPrefab = pickUpConfig.CoinPrefab;
            _medicalKitPrefab = pickUpConfig.MedicalKitPrefab;

            _random = new System.Random(_seed);
        }

        private Enemy CreateEnemy(Vector3 position) =>
            _container.Instantiate(_enemyPrefab, position, Quaternion.identity, _sceneData.EnemyParent);

        private void CreatePickUp<T>(PickUpMarker marker, T prefab) where T : PickUp
        {
            if (CanCreate(marker.SpawnChance) == false)
                return;

            _container.Instantiate<T>(
                prefab, marker.transform.position, Quaternion.identity, _sceneData.PickUpParent);
        }

        private bool CanCreate(float chance) =>
            chance >= _random.NextDouble();
    }

    public partial class GameFactory : IGameFactory
    {
        public void CreatePlayer()
        {
            Player player = _container.Instantiate(
               _playerPrefab, _sceneData.PlayerMarker.transform.position, Quaternion.identity);
            _sceneData.VirtualCamera.Follow = player.transform;
        }

        public void CreateEnemies()
        {
            Enemy enemy = null;

            foreach (EnemyMarker marker in _sceneData.EnemyMarkers)
            {
                enemy = CreateEnemy(marker.transform.position);
                enemy.Initialize(marker.PatrolRoute);
            }
        }

        public void CreateCoins()
        {
            foreach (PickUpMarker marker in _sceneData.CoinMarkers)
                CreatePickUp(marker, _coinPrefab);
        }

        public void CreateMedicalKits()
        {
            foreach (PickUpMarker marker in _sceneData.MedicalKitMarkers)
                CreatePickUp(marker, _medicalKitPrefab);
        }
    }
}
