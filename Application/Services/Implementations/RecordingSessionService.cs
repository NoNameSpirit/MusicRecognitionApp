using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecordingSessionService : IRecordingSessionService
    {
        public event Action<int> RecordingSession;

        private CancellationTokenSource? _cts;

        private readonly IRecorderService _recorderService;

        public RecordingSessionService(IRecorderService recorderService)
        {
            _recorderService = recorderService;
        }

        public async Task<string?> StartRecordingAsync()
        {
            if (_cts != null)
                throw new InvalidOperationException("Already recording");

            _cts = new CancellationTokenSource();

            _recorderService.RecordingProgress += OnRecordingSession;
            
            try
            {
                return await _recorderService.RecordAudioFromMicrophoneAsync(15, _cts.Token);
            }
            finally
            {
                _recorderService.RecordingProgress -= OnRecordingSession;
                _cts?.Dispose();
                _cts = null;    
            }
        }

        public void StopRecording()
        {
            _cts?.Cancel();
        }

        private void OnRecordingSession(int progress)
        {
            RecordingSession?.Invoke(progress);
        }
    }
}
