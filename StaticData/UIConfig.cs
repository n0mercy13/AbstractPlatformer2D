using System;
using UnityEngine;
using Codebase.UI;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [Serializable]
    public class UIConfig
    {
        [field: SerializeField] public WindowView[] UIPrefabs { get; private set; }
        [field: SerializeField] public HealthBarView HealthBarPrefab { get; private set; }
    }
}