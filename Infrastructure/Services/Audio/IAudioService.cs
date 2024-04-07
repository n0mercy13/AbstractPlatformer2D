using Codebase.Logic;

namespace Codebase.Infrastructure
{
    public interface IAudioService
    {
        void PlayMusic();
        void PlaySFX(AudioElementTypes audioType);
    }
}