using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.StaticData;
using Codebase.Logic;

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
            RegisterEntryPoints(builder);
            RegisterServices(builder);
            RegisterManagers(builder);
            RegisterComponents(builder);
        }

        private void RegisterManagers(IContainerBuilder builder)
        {
            builder
                .Register<UIManager>(Lifetime.Singleton)
                .AsSelf();
        }

        private static void RegisterComponents(IContainerBuilder builder)
        {
            builder
                .Register<Health>(Lifetime.Transient)
                .AsImplementedInterfaces();
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
                .Register<AudioService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<GameFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterEntryPoints(IContainerBuilder builder)
        {
            builder
                .RegisterEntryPoint<Bootstrap>(Lifetime.Singleton)
                .AsSelf();
            builder
                .RegisterEntryPoint<HealthViewManager>(Lifetime.Singleton)
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
                .RegisterInstance(_gameConfig.AudioConfig);
            builder
                .RegisterInstance(_gameConfig.UIConfig);
            builder
                .RegisterInstance(_sceneData);
        }
    }
}