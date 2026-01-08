using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Audio.Interfaces
{
    public interface IAudioHashGenerator
    {
        public List<AudioHash> GenerateHashes(List<Peak> peaks, int songId = 0);
    }
}
