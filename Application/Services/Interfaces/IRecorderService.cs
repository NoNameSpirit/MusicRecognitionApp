namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IRecorderService 
    {
        event Action<int> RecordingProgress;

        Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15, CancellationToken cancellationToken = default);
    }
}
