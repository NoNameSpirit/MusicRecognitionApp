using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Interfaces.Audio
{
    public interface IAudioHashGenerator
    {
        public List<AudioHash> GenerateHashes(List<Peak> peaks, int songId = 0);
    }
}
