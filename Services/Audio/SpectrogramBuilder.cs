using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;

namespace MusicRecognitionApp.Services.Audio
{
    public class SpectrogramBuilder : ISpectrogramBuilder
    {
        private const int WindowSize = 4096;
        private const int WindowGap = 2048;

        public SpectrogramData ProcessAudio(float[] audioSamples, int sampleRate)
        {
            double[,] spectrogram = BuildSpectrogram(audioSamples, sampleRate);    
            double[] frequencyAxis = GetFrequencyAxis(sampleRate);                 
            double[] timeAxis = GetTimeAxis(spectrogram.GetLength(0), sampleRate); 

            return new SpectrogramData
            {
                Spectrogram = spectrogram,
                FrequencyAxis = frequencyAxis,
                TimeAxis = timeAxis,
                SampleRate = sampleRate
            };
        }

        private List<float[]> CreateWindows(float[] audioSamples)
        {
            List<float[]> windows = new List<float[]>();

            for (int start = 0; start + WindowSize <= audioSamples.Length; start += WindowGap)
            {
                float[] window = new float[WindowSize];

                Array.Copy(audioSamples, start, window, 0, WindowSize);
                ApplyHannWindow(window); 
                windows.Add(window);
            }

            return windows;
        }

        private void ApplyHannWindow(float[] window)
        {
            for (int i = 0; i < window.Length; i++)
            {
                double hannValue = 0.5 * (1 - Math.Cos(2 * Math.PI * i / (window.Length - 1))); 
                window[i] *= (float)hannValue;
            }
        }

        private double[] ComputeFFT(float[] window)
        {
            double[] doubleWindow = new double[window.Length];
            for (int i = 0; i < window.Length; i++)
            {
                doubleWindow[i] = window[i];
            }

            System.Numerics.Complex[] fftResult = FftSharp.FFT.Forward(doubleWindow); 

            int halfLength = fftResult.Length / 2; 
            double[] magnitudes = new double[halfLength];

            for (int i = 0; i < halfLength; i++)
            {
                magnitudes[i] = fftResult[i].Magnitude; 
            }

            return magnitudes;
        }

        private double[,] BuildSpectrogram(float[] audioSamples, int sampleRate)
        {
            List<float[]> windows = CreateWindows(audioSamples);

            int numTimeFrames = windows.Count;
            int numFreqBins = WindowSize / 2; 
            double[,] spectrogram = new double[numTimeFrames, numFreqBins];

            for (int windowIndex = 0; windowIndex < numTimeFrames; windowIndex++)
            {
                double[] magnitudes = ComputeFFT(windows[windowIndex]);

                for (int freqBin = 0; freqBin < numFreqBins; freqBin++)
                {
                    spectrogram[windowIndex, freqBin] = magnitudes[freqBin];
                }
            }

            return spectrogram;
        }

        private double[] GetFrequencyAxis(int sampleRate)
        {
            int numFreqBins = WindowSize / 2;
            double[] frequencies = new double[numFreqBins];

            for (int i = 0; i < numFreqBins; i++)
            {
                frequencies[i] = (double)(i * sampleRate) / WindowSize;
            }

            return frequencies;
        }

        private double[] GetTimeAxis(int numWindows, int sampleRate)
        {
            double[] timeAxis = new double[numWindows];

            for (int i = 0; i < numWindows; i++)
            {
                timeAxis[i] = (double)(i * WindowGap) / sampleRate; 
            }

            return timeAxis;
        }
    }
}
