using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Services.Audio.Interfaces
{
    public interface IAudioHashGenerator
    {
        public List<AudioHash> GenerateHashes(List<Peak> peaks, int songId = 0);

        public List<(int songId, int matches, double confidence)> FindMatches(List<AudioHash> queryHashes, Dictionary<uint, List<AudioHash>> database);
    }
}
