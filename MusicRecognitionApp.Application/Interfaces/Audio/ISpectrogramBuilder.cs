using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Interfaces.Audio
{
    public interface ISpectrogramBuilder
    {
        public SpectrogramData ProcessAudio(float[] audioSamples, int sampleRate);
    }
}
