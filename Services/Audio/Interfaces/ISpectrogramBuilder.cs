using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Services.Audio.Interfaces
{
    public interface ISpectrogramBuilder
    {
        public SpectrogramData ProcessAudio(float[] audioSamples, int sampleRate);
    }
}
