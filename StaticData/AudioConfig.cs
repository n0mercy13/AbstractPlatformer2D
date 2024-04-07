using System;
using UnityEngine;

namespace Codebase.StaticData
{
    [Serializable]
    public class AudioConfig
    {
        [field: SerializeField] public AudioElement[] AudioElements { get; private set; } 
    }
}