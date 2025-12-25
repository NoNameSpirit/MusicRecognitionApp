namespace MusicRecognitionApp.Infrastructure.Audio.Interfaces
{
    public interface IAudioProcessor
    {
        public float[] PreprocessAudio(string filePath);
    }
}
