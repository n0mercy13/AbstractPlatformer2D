using Codebase.Logic.EnemyComponents;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField, Header("Instantiation")] public Enemy Prefab { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f), Header("Movements")] public float PatrolSpeed { get; private set; }
        [field: SerializeField, Range(0.0f, 15.0f)] public float PursueSpeed { get; private set; }
        [field: SerializeField, Range(0, 50), Header("Stats")] public int MaxHealth { get; private set; }
        [field: SerializeField, Range(0, 10), Header("Attack")] public int Damage { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] public float AttackSpeed { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] public float AttackRadius { get; private set; }

        [field: SerializeField, Range(0.0f, 10.0f), Header("Detection")] public float DetectionRadius { get; private set; }
    }
}