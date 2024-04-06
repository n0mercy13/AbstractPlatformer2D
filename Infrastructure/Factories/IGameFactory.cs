using Codebase.Logic.PlayerComponents;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        void CreatePlayer();
        void CreateEnemies();
        void CreateCoins();
    }
}