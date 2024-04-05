using Codebase.Logic.Player;
using Codebase.StaticData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public partial class GameFactory
    {
        private readonly IObjectResolver _container;
        private readonly Player _playerPrefab;
        private readonly Vector3 _playerInitialPosition;

        public GameFactory(IObjectResolver container, PlayerConfig playerConfig) 
        { 
            _container = container;
            _playerPrefab = playerConfig.Prefab;
            _playerInitialPosition = playerConfig.InitialPosition;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public Player CreatePlayer() => 
            _container.Instantiate<Player>(_playerPrefab, _playerInitialPosition, Quaternion.identity);
    }
}
