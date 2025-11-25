namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioRecognition
    {
        Task<List<(int songId, string title, string artist, int matches, double confidence)>> RecognizeFromMicrophoneAsync();
    }
}
