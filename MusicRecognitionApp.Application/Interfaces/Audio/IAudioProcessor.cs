namespace MusicRecognitionApp.Application.Interfaces.Audio
{
    public interface IAudioProcessor
    {
        public float[] PreprocessAudio(Stream audioStream);
    }
}
