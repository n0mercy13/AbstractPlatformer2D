﻿using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.Logic;
using Codebase.Logic.EnemyComponents;
using Codebase.Logic.PlayerComponents;
using Codebase.StaticData;
using Codebase.UI;

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
        private readonly RectTransform _uiRoot;
        private readonly WindowView[] _uiPrefabs;
        private readonly HealthBarView _healthBarPrefab;

        public GameFactory(
            IObjectResolver container,
            SceneData sceneData,
            PlayerConfig playerConfig,
            EnemyConfig enemyConfig,
            PickUpsConfig pickUpConfig,
            UIConfig uiConfig)
        {
            _container = container;
            _sceneData = sceneData;
            _playerPrefab = playerConfig.Prefab;
            _enemyPrefab = enemyConfig.Prefab;
            _coinPrefab = pickUpConfig.CoinPrefab;
            _medicalKitPrefab = pickUpConfig.MedicalKitPrefab;
            _uiRoot = sceneData.UIRoot;
            _uiPrefabs = uiConfig.UIPrefabs;
            _healthBarPrefab = uiConfig.HealthBarPrefab;

            _random = new System.Random(_seed);
        }

        private void CreatePickUp<T>(PickUpMarker marker, T prefab) where T : PickUp
        {
            if (CanCreate(marker.SpawnChance) == false)
                return;

            _container.Instantiate<T>(
                prefab, marker.transform.position, Quaternion.identity, _sceneData.PickUpParent);
        }

        private bool CanCreate(float chance) => chance >= _random.NextDouble();
    }

    public partial class GameFactory : IGameFactory
    {
        public Player CreatePlayer()
        {
            Player player = _container.Instantiate(
               _playerPrefab, _sceneData.PlayerMarker.transform.position, Quaternion.identity);
            _sceneData.VirtualCamera.Follow = player.transform;

            return player;
        }

        public Enemy CreateEnemy(Vector3 position) =>
            _container.Instantiate(_enemyPrefab, position, Quaternion.identity, _sceneData.EnemyParent);

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

        public WindowView[] CreateUI()
        {
            WindowView uiWindow = null;
            WindowView[] createdUI = new WindowView[_uiPrefabs.Length];

            for(int i = 0; i < _uiPrefabs.Length; i++)
            {
                uiWindow = _container.Instantiate(_uiPrefabs[i], _uiRoot);
                uiWindow.Close();
                createdUI[i] = uiWindow;
            }

            return createdUI;
        }

        public HealthBarView CreateHealthBar() => 
            _container.Instantiate(_healthBarPrefab, _uiRoot);
    }
}
