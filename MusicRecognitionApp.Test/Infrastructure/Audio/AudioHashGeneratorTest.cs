using FluentAssertions;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Audio.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Audio
{
    public class AudioHashGeneratorTest
    {
        private readonly AudioHashGenerator _audioHashGenerator;

        public AudioHashGeneratorTest()
        {
            _audioHashGenerator = new AudioHashGenerator();
        }

        [Theory]
        [InlineData(1.5, 100.0, 1.0, 100.0)]
        [InlineData(1.0, 100.0, 1.0, 2000.0)]
        public void GenerateGashes_NotValidPeaks_EmptyResult(double anchorTime, double anchorFreq,
                                                             double targetTime, double targetFreq)
        {
            //Arrange
            var peaks = new List<Peak> {
                new Peak() { TimeSeconds = anchorTime, FrequencyHz = anchorFreq, Magnitude = 100 },
                new Peak() { TimeSeconds = targetTime, FrequencyHz = targetFreq, Magnitude = 100 },
            };

            //Act
            var result = _audioHashGenerator.GenerateHashes(peaks);

            //Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1.0, 100.0, 1.3, 100.0)]
        public void GenerateHashes_ValidPeaks_NotEmptyResult(double anchorTime, double anchorFreq,
                                                             double targetTime, double targetFreq)
        {
            //Arrange
            var peaks = new List<Peak> {
                new Peak() { TimeSeconds = anchorTime, FrequencyHz = anchorFreq, Magnitude = 100 },
                new Peak() { TimeSeconds = targetTime, FrequencyHz = targetFreq, Magnitude = 100 },
            };

            //Act
            var result = _audioHashGenerator.GenerateHashes(peaks);

            //Assert
            result.Should().NotBeEmpty();
        }
    }
}