namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IRecordingSessionService
    {
        event Action<int> RecordingSession;

        Task<string?> StartRecordingAsync();

        void StopRecording();
    }
}
