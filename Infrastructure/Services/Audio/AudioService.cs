using System;
using System.Collections.Generic;
using Codebase.Logic;
using Codebase.StaticData;
using UnityEngine.Audio;

namespace Codebase.Infrastructure
{
    public partial class AudioService
    {
        private readonly IAudioPlayer _player;
        private readonly AudioMixer _mixer;
        private readonly Dictionary<AudioElementTypes, AudioElement> _audioClips;

        public AudioService(SceneData data, AudioConfig config)
        {
            _player = data.AudioPlayer;
            _mixer = config.AudioMixer;
            _audioClips = new Dictionary<AudioElementTypes, AudioElement>();

            foreach(AudioElement audio in config.AudioElements)
            {
                _audioClips.Add(audio.Type, audio);
            }
        }
    }

    public partial class AudioService : IAudioService
    {
        public void PlayMusic()
        {
            if(_audioClips.TryGetValue(AudioElementTypes.Music_MainTheme, out AudioElement music)) 
                _player.PlayMusic(music);
            else
                throw new ArgumentOutOfRangeException(
                    $"Music type: {AudioElementTypes.Music_MainTheme} was not found");
        }

        public void PlaySFX(AudioElementTypes audioType)
        {
            if(_audioClips.TryGetValue(audioType, out AudioElement sfx))
                _player.PlaySFX(sfx);
            else
                throw new ArgumentOutOfRangeException(
                    $"SFX type: {audioType} was not found");
        }

        public void SetVolume(UIElementTypes sliderType, float volume)
        {
            PlaySFX(AudioElementTypes.SFX_UI_ElementPressed);

            switch (sliderType)
            {
                case UIElementTypes.UI_Slider_Volume_Master:
                    _mixer.SetFloat(Constants.Audio.Parameters.MasterVolume, volume);
                    break;

                case UIElementTypes.UI_Slider_Volume_SFX:
                    _mixer.SetFloat(Constants.Audio.Parameters.SFXVolume, volume);
                    break;

                case UIElementTypes.UI_Slider_Volume_Music:
                    _mixer.SetFloat(Constants.Audio.Parameters.MusicVolume, volume);
                    break;

                default:
                    throw new InvalidOperationException($"UI type: {sliderType} cannot be handled!");
            }
        }
    }
}
