using MusicRecognitionApp.Infrastructure.Audio.Interfaces;
using NAudio.Dsp;
using NAudio.Wave;

namespace MusicRecognitionApp.Infrastructure.Audio.Implementations
{
    public class AudioProcessor : IAudioProcessor
    {
        private float[] audioSamples;
        private int sampleRate;

        public float[] PreprocessAudio(string filePath)
        {
            float[] samples = LoadAudioFile(filePath, out int channels);
            samples = ConvertToMono(samples, channels);
            samples = ApplyLowPassFilter(samples, 5000f);
            samples = DownsampleAudio(samples, 4);
            return samples;
        }

        private float[] LoadAudioFile(string filePath, out int channels)
        {
            using (var audioFileReader = new AudioFileReader(filePath))
            {
                channels = audioFileReader.WaveFormat.Channels;

                sampleRate = audioFileReader.WaveFormat.SampleRate;

                int totalSamples = (int)(audioFileReader.Length / (audioFileReader.WaveFormat.BitsPerSample / 8));

                audioSamples = new float[totalSamples];

                audioFileReader.Read(audioSamples, 0, totalSamples);
            }

            return audioSamples;
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

        private float[] ApplyLowPassFilter(float[] samples, float cutoffFreency = 5000f)
        {
            var filter = BiQuadFilter.LowPassFilter(sampleRate, cutoffFreency, 1.0f);

            float[] filteredSamples = new float[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                filteredSamples[i] = filter.Transform(samples[i]);
            }

            return filteredSamples;
        }

        private float[] DownsampleAudio(float[] samples, int downsampleFactor = 4)
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
