using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SceneData _sceneData;

        private void OnValidate()
        {
            if (_gameConfig == null)
                throw new ArgumentNullException(nameof(_gameConfig));

            if (_sceneData == null)
                throw new ArgumentNullException(nameof(_sceneData));
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStaticData(builder);
            RegisterPrefabs(builder);
            RegisterEntryPoint(builder);
            RegisterServices(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder
                .Register<InputActions>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<InputService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<GameFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder
                .RegisterEntryPoint<Bootstrap>(Lifetime.Singleton)
                .AsSelf();
        }

        private void RegisterPrefabs(IContainerBuilder builder)
        {
            builder
                .RegisterComponentInNewPrefab(_gameConfig.PlayerConfig.Prefab, Lifetime.Scoped);
            builder
                .RegisterComponentInNewPrefab(_gameConfig.EnemyConfig.Prefab, Lifetime.Scoped);
            builder
                .RegisterComponentInNewPrefab(_gameConfig.PickUpsConfig.CoinPrefab, Lifetime.Scoped);
        }

        private void RegisterStaticData(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(_gameConfig.PlayerConfig);
            builder
                .RegisterInstance(_gameConfig.EnemyConfig);
            builder
                .RegisterInstance(_gameConfig.PickUpsConfig);
            builder
                .RegisterInstance(_sceneData);
        }
    }
}