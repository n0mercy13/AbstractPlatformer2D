using Codebase.Logic.EnemyComponents;
using Codebase.Logic.PlayerComponents;
using Codebase.UI;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        Player CreatePlayer();
        Enemy CreateEnemy(Vector3 position);
        void CreateCoins();
        void CreateMedicalKits();
        WindowView[] CreateUI();
        HealthBarView CreateHealthBar();
    }
}