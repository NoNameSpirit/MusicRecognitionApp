using MusicRecognitionApp.Services.Interfaces;
using NAudio.Wave;

namespace MusicRecognitionApp.Services
{
    public class AudioRecorderService : IAudioRecorder
    {
        public bool IsRecording { get; private set; }
        public event Action<int> RecordingProgress;

        public async Task<string> RecordAudioFromMicrophoneAsync(int durationSeconds = 15)
        {
            IsRecording = true;
            try
            {
                var outputFilePath = @"D:\NewShazam\recorded.wav";
                int deviceNumber = 0;

                var sourceStream = new WaveInEvent();
                WaveInCapabilities deviceCaps = WaveInEvent.GetCapabilities(deviceNumber);

                sourceStream.DeviceNumber = deviceNumber;
                sourceStream.WaveFormat = new WaveFormat(44100, deviceCaps.Channels);
                sourceStream.BufferMilliseconds = 50;
                sourceStream.NumberOfBuffers = 3;

                var waveWriter = new WaveFileWriter(outputFilePath, sourceStream.WaveFormat);

                sourceStream.DataAvailable += (sender, e) =>
                {
                    waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                    waveWriter.Flush();
                };

                sourceStream.StartRecording();

                for (int i = 0; i < durationSeconds-1; i++)
                {
                    await Task.Delay(1000);

                    int amountOfProgress = (i + 1) * 100 / durationSeconds;
                    int progress = amountOfProgress > 100 ? 100 : amountOfProgress;
                    RecordingProgress(progress);
                }

                sourceStream.StopRecording();
                await Task.Delay(2000);

                waveWriter.Dispose();
                sourceStream.Dispose();

                return outputFilePath;
            }
            finally
            {
                IsRecording = false;
            }
        }
    }
}
