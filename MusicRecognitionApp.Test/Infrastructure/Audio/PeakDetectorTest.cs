using FluentAssertions;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Audio.Implementations;
using System.Drawing;

namespace MusicRecognitionApp.Test.Infrastructure.Audio
{
    public class PeakDetectorTest
    {
        private readonly PeakDetector _peakDetector;

        public PeakDetectorTest()
        {
            _peakDetector = new PeakDetector();
        }

        [Fact]
        public void PeakDetector_ValidSpectrogramValues_PeaksHaveValues()
        {
            //Arrange
            var spectrogramData = CreateSpectrogramDataWithValue(0.2);
            spectrogramData.Spectrogram[5, 5] = 1.0;

            //Act
            var peaks = _peakDetector.ProcessPeekDetector(spectrogramData);

            //Assert
            peaks.Should().NotBeEmpty();
        }

        [Fact]
        public void PeakDetector_NotValidSpectrogramValues_PeaksHaveNotValues()
        {
            //Arrange
            var spectrogramData = CreateSpectrogramDataWithValue(0.01);

            //Act
            var peaks = _peakDetector.ProcessPeekDetector(spectrogramData);

            //Assert
            peaks.Should().BeEmpty();
        }

        private SpectrogramData CreateSpectrogramDataWithValue(double value, int size = 10)
        {
            var spectrogramData = new SpectrogramData
            {
                Spectrogram = new double[size, size],
                FrequencyAxis = new double[size],
                TimeAxis = new double[size],
                SampleRate = 16000,
            };

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    spectrogramData.Spectrogram[i, j] = value;
                }
            }

            return spectrogramData;
        }
    }
}