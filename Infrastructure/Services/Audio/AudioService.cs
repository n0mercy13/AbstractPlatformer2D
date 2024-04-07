using System;
using System.Collections.Generic;
using Codebase.Logic;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class AudioService
    {
        private readonly IAudioPlayer _player;
        private readonly Dictionary<AudioElementTypes, AudioElement> _audioClips;

        public AudioService(SceneData data, AudioConfig config)
        {
            _player = data.AudioPlayer;
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
    }
}
