using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Codebase.StaticData
{
    [Serializable]
    public class AudioConfig
    {
        [field: SerializeField] public AudioElement[] AudioElements { get; private set; }
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
    }
}