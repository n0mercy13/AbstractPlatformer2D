using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [Serializable]
    public class AbilityData
    {
        [field: SerializeField] public AbilityTypes Type { get; private set; }
        [field: SerializeField, Range(0.0f, 5.0f)] protected float Range;
        [field: SerializeField, Range(0.0f, 5.0f)] protected float Duration;
    }
}
