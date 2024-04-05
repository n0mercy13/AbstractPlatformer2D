using Cinemachine;
using Codebase.Logic.Player;
using VContainer.Unity;

namespace Codebase.Infrastructure
{
    public class Bootstrap : IStartable
    {
        private readonly IGameFactory _gameFactory;
        private readonly CinemachineVirtualCamera _virtualCamera;

        public Bootstrap(IGameFactory gameFactory, CinemachineVirtualCamera virtualCamera)
        {
            _gameFactory = gameFactory;
            _virtualCamera = virtualCamera;
        }

        public void Start()
        {
            Player player = _gameFactory.CreatePlayer();
            _virtualCamera.Follow = player.transform;
        }
    }
}