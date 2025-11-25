namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioRecorder
    {
        bool IsRecording { get; }
        event Action<int> RecordingProgress;
        
        Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15);
    }
}
