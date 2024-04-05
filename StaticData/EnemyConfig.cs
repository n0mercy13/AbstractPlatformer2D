using System;
using UnityEngine;
using Codebase.Logic.Enemy;

namespace Codebase.StaticData
{
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField, Header("Instantiation")] public Enemy Prefab {  get; private set; }
    }
}