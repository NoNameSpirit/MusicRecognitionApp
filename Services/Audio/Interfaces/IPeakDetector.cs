using MusicRecognitionApp.Data;

namespace MusicRecognitionApp.Services.Audio.Interfaces
{
    public interface IPeakDetector
    {
        public List<Peak> ProcessPeekDetector(SpectrogramData spectrogramData);
    }
}
