using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Audio.Interfaces
{
    public interface IPeakDetector
    {
        public List<Peak> ProcessPeekDetector(SpectrogramData spectrogramData);
    }
}
