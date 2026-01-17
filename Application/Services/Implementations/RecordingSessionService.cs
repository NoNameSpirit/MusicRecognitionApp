using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecordingSessionService : IRecordingSessionService
    {
        public event Action<int> RecordingSession;

        private CancellationTokenSource? _cts;

        private readonly IServiceProvider _serviceProvider;

        public RecordingSessionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<string?> StartRecordingAsync()
        {
            if (_cts != null)
                throw new InvalidOperationException("Already recording");

            _cts = new CancellationTokenSource();

            var recorder = _serviceProvider.GetRequiredService<IAudioRecorderService>();

            recorder.RecordingProgress += OnRecordingSession;
            
            try
            {
                return await recorder.RecordAudioFromMicrophoneAsync(15, _cts.Token);
            }
            finally
            {
                recorder.RecordingProgress -= OnRecordingSession;
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
