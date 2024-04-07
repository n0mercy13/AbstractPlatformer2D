using System;
using UnityEngine;
using Codebase.UI;

namespace Codebase.StaticData
{
    [Serializable]
    public class UIConfig
    {
        [field: SerializeField] public UI_Window[] UIPrefabs { get; private set; }  
    }
}