using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [Serializable]
    public class AudioElement
    {
        [field: SerializeField] public AudioElementTypes Type { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField, Range(0.0f, 1.0f)] public float DefaultSound { get; private set; } = 1.0f;
        [field: SerializeField, Range(-3.0f, 3.0f)] public float DefaultPitch { get; private set; } = 1.0f;
    }
}