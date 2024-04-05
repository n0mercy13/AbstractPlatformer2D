using Codebase.Logic.Player;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        Player CreatePlayer();
    }
}