using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Interfaces.Audio
{
    public interface IPeakDetector
    {
        public List<Peak> ProcessPeekDetector(SpectrogramData spectrogramData);
    }
}
