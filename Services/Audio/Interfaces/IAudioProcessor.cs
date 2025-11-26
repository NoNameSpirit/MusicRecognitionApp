namespace MusicRecognitionApp.Services.Audio.Interfaces
{
    public interface IAudioProcessor
    {
        public float[] PreprocessAudio(string filePath);
    }
}
