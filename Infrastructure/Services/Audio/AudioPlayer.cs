using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _musicSource;

        private void OnValidate()
        {
            if(_sfxSource == null)
                throw new ArgumentNullException(nameof(_sfxSource));

            if(_musicSource == null)
                throw new ArgumentNullException(nameof(_musicSource));
        }

        private void Awake()
        {
            _sfxSource.playOnAwake = false;
            _sfxSource.loop = false;
            _musicSource.playOnAwake = false;
            _musicSource.loop = true;
        }
    }

    public partial class AudioPlayer : IAudioPlayer
    {
        public void PlayMusic(AudioElement audioElement)
        {
            _musicSource.volume = audioElement.DefaultVolume;
            _musicSource.pitch = audioElement.DefaultPitch;
            _musicSource.clip = audioElement.AudioClip;
            _musicSource.Play();
        }

        public void PlaySFX(AudioElement audioElement)
        {
            _sfxSource.volume = audioElement.DefaultVolume;
            _sfxSource.pitch = audioElement.DefaultPitch;
            _sfxSource.PlayOneShot(audioElement.AudioClip);
        }
    }
}
