using MusicRecognitionApp.Application.Services.Interfaces;
using NAudio.Wave;
using System.Reflection;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AudioRecorderService : IAudioRecorder
    {
        public bool IsRecording { get; private set; }
        public event Action<int> RecordingProgress;

        private WaveInEvent _sourceStream;
        private WaveFileWriter _waveWriter;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly object _lockObject = new object();

        public async Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15, CancellationToken cancellationToken = default)
        {
            string outputFilePath = null;
            IsRecording = true;
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                var exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                outputFilePath = Path.Combine(exeDirectory, $"recorded_audio_{DateTime.Now:yyyyMMdd_HHmmssfff}.wav");
                int deviceNumber = 0;

                WaveInCapabilities deviceCaps = WaveInEvent.GetCapabilities(deviceNumber);

                _sourceStream = new WaveInEvent();
                _sourceStream.DeviceNumber = deviceNumber;
                _sourceStream.WaveFormat = new WaveFormat(44100, deviceCaps.Channels);
                _sourceStream.BufferMilliseconds = 50;
                _sourceStream.NumberOfBuffers = 3;

                _waveWriter = new WaveFileWriter(outputFilePath, _sourceStream.WaveFormat);

                _sourceStream.DataAvailable += (sender, e) =>
                {
                    lock (_lockObject)
                    {
                        if (_waveWriter != null)
                        {
                            _waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                            _waveWriter.Flush();
                        }
                    }
                };

                _sourceStream.StartRecording();

                for (int i = 0; i < durationSeconds; i++)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    await Task.Delay(1000);

                    int progress = (i + 1) * 100 / durationSeconds;
                    RecordingProgress?.Invoke(progress);
                }

                if (_sourceStream != null)
                {
                    _sourceStream.StopRecording();
                    await Task.Delay(500);
                }

                lock (_lockObject)
                {
                    if (_waveWriter != null)
                    {
                        _waveWriter.Dispose();
                        _waveWriter = null;
                    }
                }

                if (_sourceStream != null)
                {
                    _sourceStream.Dispose();
                    _sourceStream = null;
                }

                return _cancellationTokenSource.Token.IsCancellationRequested ? null : outputFilePath;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                if (_cancellationTokenSource?.Token.IsCancellationRequested == true && outputFilePath != null)
                {
                    if (File.Exists(outputFilePath))
                        File.Delete(outputFilePath);
                }

                IsRecording = false;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}
