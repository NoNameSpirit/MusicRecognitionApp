using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Audio.Interfaces
{
    public interface ISpectrogramBuilder
    {
        public SpectrogramData ProcessAudio(float[] audioSamples, int sampleRate);
    }
}
