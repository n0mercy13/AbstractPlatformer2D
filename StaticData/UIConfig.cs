using System;
using UnityEngine;
using Codebase.UI;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [Serializable]
    public class UIConfig
    {
        [field: SerializeField] public UI_Window[] UIPrefabs { get; private set; }
        [field: SerializeField] public UI_HealthBar HealthBarPrefab { get; private set; }
    }
}