using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class RaycastConfig
    {
        [field: SerializeField] public LayerMask PlayerMask { get; private set; }
        [field: SerializeField] public LayerMask EnemyMask { get; private set; }
    }
}