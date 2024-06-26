﻿using Codebase.Logic.PlayerComponents;
using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class PlayerConfig
    {
        [field: SerializeField, Range(0, 100), Header("Stats")] public int MaxHealth { get; private set; }
        [field: SerializeField, Range(0, 10), Header("Attack")] public int Damage { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] public float AttackSpeed { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] public float AttackRadius { get; private set; }
        [field: SerializeField, Range(0.0f, 10.0f), Header("Horizontal Movements")] public float WalkSpeed { get; private set; }
        [field: SerializeField, Range(0.0f, 2.0f)] public float WalkModifier { get; private set; } = 1.0f;
        [field: SerializeField, Range(1.0f, 3.0f)] public float RunModifier { get; private set; }
        [field: SerializeField, Range(0.1f, 1.0f)] public float InAirModifier { get; private set; }
        [field: SerializeField, Range(0.0f, 2.0f), Header("Vertical Movements")] public float JumpDuration { get; private set; }
        [field: SerializeField, Range(10.0f, 30.0f)] public float JumpVelocity { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] public float GravityModifier { get; private set; }
        [field: SerializeField, Header("Instantiation")] public Player Prefab { get; private set; }
    }
}
