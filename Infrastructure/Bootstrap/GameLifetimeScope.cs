using Cinemachine;
using Codebase.StaticData;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private void OnValidate()
        {
            if (_gameConfig == null)
                throw new ArgumentNullException(nameof(_gameConfig));

            if (_virtualCamera == null)
                throw new ArgumentNullException(nameof(_virtualCamera));
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(_gameConfig.PlayerConfig);
            builder
                .RegisterInstance(_gameConfig.EnemyConfig);
            builder 
                .RegisterInstance(_virtualCamera);
            builder
                .RegisterComponentInNewPrefab(_gameConfig.PlayerConfig.Prefab, Lifetime.Scoped)
                .AsSelf();

            builder
                .RegisterEntryPoint<Bootstrap>(Lifetime.Singleton)
                .AsSelf();

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
    }
}