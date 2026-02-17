using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecordingSessionService : IRecordingSessionService
    {
        public event Action<int> RecordingSession;

        private CancellationTokenSource? _cts;

        private readonly IServiceScopeFactory _scopeFactory;

        public RecordingSessionService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<string?> StartRecordingAsync()
        {
            if (_cts != null)
                throw new InvalidOperationException("Already recording");
            using (var scope = _scopeFactory.CreateScope())
            {
                var _recorderService = scope.ServiceProvider.GetRequiredService<IRecorderService>();

                try
                {
                    _cts = new CancellationTokenSource();

                    _recorderService.RecordingProgress += OnRecordingSession;

                    string? recordedAudioFile = await _recorderService.RecordAudioFromMicrophoneAsync(15, _cts.Token);

                    return recordedAudioFile;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                finally
                {
                    _recorderService.RecordingProgress -= OnRecordingSession;
                    _cts?.Dispose();
                    _cts = null;
                }
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
