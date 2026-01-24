using MusicRecognitionApp.Application.Services.Interfaces;
using NAudio.Wave;
using System.Reflection;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecorderService : IRecorderService
    {
        public event Action<int> RecordingProgress;

        private readonly object _lockObject = new object();
        private WaveFileWriter _currentWriter;

        public async Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15, CancellationToken cancellationToken = default)
        {
            string outputFilePath = null;
            bool success = false;

            var exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            outputFilePath = Path.Combine(exeDirectory, $"recorded_audio_{DateTime.Now:yyyyMMdd_HHmmssfff}.wav");

            WaveInEvent? sourceStream = null;
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                int deviceNumber = 0;
                WaveInCapabilities deviceCaps = WaveInEvent.GetCapabilities(deviceNumber);

                sourceStream = new WaveInEvent
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new WaveFormat(44100, deviceCaps.Channels),
                    BufferMilliseconds = 50,
                    NumberOfBuffers = 3,
                };

                using var waveWriter = new WaveFileWriter(outputFilePath, sourceStream.WaveFormat);
                _currentWriter = waveWriter;

                sourceStream.DataAvailable += OnDataAvailable;
                sourceStream.StartRecording();

                for (int i = 0; i < durationSeconds; i++)
                {
                    await Task.Delay(1000, cts.Token);

                    int progress = (i + 1) * 100 / durationSeconds;
                    RecordingProgress?.Invoke(progress);
                }
                success = !cts.Token.IsCancellationRequested;

                return success ? outputFilePath : null;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                if(sourceStream != null) 
                {
                    sourceStream.DataAvailable -= OnDataAvailable;
                    sourceStream.Dispose();
                }

                _currentWriter = null;

                if (!success)
                    CleanupFile(outputFilePath);
            }
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            lock (_lockObject)
            {
                if (_currentWriter != null && e.BytesRecorded > 0)
                {
                    _currentWriter.Write(e.Buffer, 0, e.BytesRecorded);
                }
            }
        }

        private void CleanupFile(string filepath)
        {
            if (filepath != null && File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
