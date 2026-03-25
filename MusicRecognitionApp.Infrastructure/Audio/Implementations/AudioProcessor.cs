using MusicRecognitionApp.Application.Interfaces.Audio;
using NAudio.Dsp;
using NAudio.Wave;

namespace MusicRecognitionApp.Infrastructure.Audio.Implementations
{
    public class AudioProcessor : IAudioProcessor
    {
        public float[] PreprocessAudio(Stream stream)
        {
            float[] samples = LoadAudioFile(stream, out int channels, out int sampleRate);
            samples = ConvertToMono(samples, channels);
            samples = ApplyLowPassFilter(samples, sampleRate, 5000f);
            samples = DownsampleAudio(samples, sampleRate, 4);
            return samples;
        }

        private float[] LoadAudioFile(Stream stream, out int channels, out int sampleRate)
        {
            using (var reader = new WaveFileReader(stream))
            {
                channels = reader.WaveFormat.Channels;
                sampleRate = reader.WaveFormat.SampleRate;

                var sampleProvider = reader.ToSampleProvider();
                List<float> samples = new List<float>();
                float[] buffer = new float[reader.WaveFormat.SampleRate * channels];
                
                int read;
                while ((read = sampleProvider.Read(buffer, 0, buffer.Length)) > 0)
                {
                    samples.AddRange(buffer.Take(read));
                }

                return samples.ToArray();
            }
        }

        private float[] ConvertToMono(float[] samples, int channels)
        {
            if (channels == 1)
                return samples;

            int monoLength = samples.Length / channels;
            float[] monoSamples = new float[monoLength];

            for (int i = 0; i < monoLength; i++)
            {
                float sum = 0.0f;
                for (int j = 0; j < channels; j++)
                {
                    sum += samples[i * channels + j];
                }

                monoSamples[i] = sum / channels;
            }

            return monoSamples;
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