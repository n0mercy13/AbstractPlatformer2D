using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public interface IAudioPlayer
    {
        void PlayMusic(AudioElement audioElement);
        void PlaySFX(AudioElement audioElement);
    }
}