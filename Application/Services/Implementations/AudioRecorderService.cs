using MusicRecognitionApp.Application.Services.Interfaces;
using NAudio.Wave;
using System;
using System.Reflection;
using System.Threading;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AudioRecorderService : IAudioRecorder, IDisposable
    {
        public bool IsRecording { get; private set; }
        public event Action<int> RecordingProgress;

        private bool _disposed = false;
        private readonly object _lockObject = new object();

        private WaveInEvent? _sourceStream;
        private WaveFileWriter? _waveWriter;
        private CancellationTokenSource? _cancellationTokenSource;

        public async Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15, CancellationToken cancellationToken = default)
        {
            string outputFilePath = null;
            bool success = false;

            if (_disposed)
                throw new ObjectDisposedException(nameof(AudioRecorderService));

            if (IsRecording)
                throw new InvalidOperationException("Recording already in progress!");

            IsRecording = true;
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                var exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                outputFilePath = Path.Combine(exeDirectory, $"recorded_audio_{DateTime.Now:yyyyMMdd_HHmmssfff}.wav");

                int deviceNumber = 0;
                WaveInCapabilities deviceCaps = WaveInEvent.GetCapabilities(deviceNumber);

                _sourceStream = new WaveInEvent
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new WaveFormat(44100, deviceCaps.Channels),
                    BufferMilliseconds = 50,
                    NumberOfBuffers = 3,
                };

                _waveWriter = new WaveFileWriter(outputFilePath, _sourceStream.WaveFormat);

                _sourceStream.DataAvailable += OnDataAvailable;
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
                success = !_cancellationTokenSource.Token.IsCancellationRequested;

                return success ? outputFilePath : null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                _sourceStream?.StopRecording();
                if (_sourceStream != null)
                {
                    _sourceStream.DataAvailable -= OnDataAvailable;
                }

                if (!success)
                    CleanupFile(outputFilePath);

                IsRecording = false;
            }
        }

        private void CleanupFile(string filepath)
        {
            if (filepath != null && File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            lock (_lockObject)
            {
                if (_waveWriter != null && e.BytesRecorded > 0)
                {
                    _waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                    _waveWriter.Flush();
                }
            }
        }

        ~AudioRecorderService()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // managed resources
            if (disposing)
            {
                if (IsRecording)
                {
                    _sourceStream?.StopRecording();
                    _cancellationTokenSource?.Cancel();
                    IsRecording = false;
                }
                
                WaveFileWriter writerToDispose = null;
                lock (_lockObject)
                {
                    writerToDispose = _waveWriter;
                    _waveWriter = null;
                }

                writerToDispose?.Dispose();
                
                _sourceStream?.Dispose();
                _sourceStream = null;
                
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
            
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
