namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioRecognition
    {
        Task<string> RecordAudioAsync(int durationTime = 15, CancellationToken cancellationToken = default);

        Task<List<(int songId, string title, string artist, int matches, double confidence)>> RecognizeFromMicrophoneAsync(string audioFilePath);

        event Action<int> AnalysisProgress;
    }
}
