using FftSharp;
using MusicRecognitionApp.Application.Interfaces.Audio;
using NAudio.Dsp;
using NAudio.Wave;

namespace MusicRecognitionApp.Infrastructure.Audio.Implementations
{
    public class AudioProcessor : IAudioProcessor
    {
        public float[] PreprocessAudio(string filePath)
        {
            float[] samples = LoadAudioFile(filePath, out int channels, out int sampleRate);
            samples = ConvertToMono(samples, channels);
            samples = ApplyLowPassFilter(samples, sampleRate, 5000f);
            samples = DownsampleAudio(samples, sampleRate, 4);
            return samples;
        }

        private float[] LoadAudioFile(string filePath, out int channels, out int sampleRate)
        {
            using (var audioFileReader = new AudioFileReader(filePath))
            {
                channels = audioFileReader.WaveFormat.Channels;

                sampleRate = audioFileReader.WaveFormat.SampleRate;

                int totalSamples = (int)(audioFileReader.Length / (audioFileReader.WaveFormat.BitsPerSample / 8));

                var audioSamples = new float[totalSamples];

                audioFileReader.Read(audioSamples, 0, totalSamples);
                return audioSamples;
            }
        }

        private float[] ConvertToMono(float[] stereoSamples, int channels)
        {
            if (channels == 1)
                return stereoSamples;

            if (channels == 2)
            {
                int monoLength = stereoSamples.Length / 2;
                float[] monoSamples = new float[monoLength];

                for (int i = 0; i < monoLength; i++)
                {
                    monoSamples[i] = (stereoSamples[i * 2] + stereoSamples[i * 2 + 1]) / 2.0f;
                }

                return monoSamples;
            }

            throw new NotSupportedException("Только моно/стерео");
        }

        private float[] ApplyLowPassFilter(float[] samples, int sampleRate, float cutoffFreency = 5000f)
        {
            var filter = BiQuadFilter.LowPassFilter(sampleRate, cutoffFreency, 1.0f);

            float[] filteredSamples = new float[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                filteredSamples[i] = filter.Transform(samples[i]);
            }

            return filteredSamples;
        }

        private float[] DownsampleAudio(float[] samples, int sampleRate, int downsampleFactor = 4)
        {
            int newLength = samples.Length / downsampleFactor;
            float[] downsampledSamples = new float[newLength];

            for (int i = 0; i < newLength; i++)
            {
                downsampledSamples[i] = samples[i * downsampleFactor];
            }

            sampleRate = sampleRate / downsampleFactor;

            return downsampledSamples;
        }
    }
}
