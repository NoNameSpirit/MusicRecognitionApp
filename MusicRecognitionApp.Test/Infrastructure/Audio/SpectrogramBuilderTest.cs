using FluentAssertions;
using MusicRecognitionApp.Infrastructure.Audio.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Audio
{
    public class SpectrogramBuilderTest
    {
        private readonly SpectrogramBuilder _spectrogramData;

        public SpectrogramBuilderTest()
        {
            _spectrogramData = new SpectrogramBuilder();
        }

        [Fact]
        public void SpectrogramBuilder_ValidSamples_SpectrogramDataIsNotNull()
        {
            //Arrange
            var audioSamples = new float[12000];
            int sampleRate = 16000;

            //Act
            var result = _spectrogramData.ProcessAudio(audioSamples, sampleRate);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void SpectrogramBuilder_ValidSamples_WithSameSampleRate()
        {
            //Arrange
            var audioSamples = new float[12000];
            int sampleRate = 16000;

            //Act
            var result = _spectrogramData.ProcessAudio(audioSamples, sampleRate);

            //Assert
            result.SampleRate.Should().Be(sampleRate);
        }

        [Fact]
        public void SpectrogramBuilder_ValidSamples_AxesNotEmpty()
        {
            //Arrange
            var audioSamples = new float[12000];
            int sampleRate = 16000;

            //Act
            var result = _spectrogramData.ProcessAudio(audioSamples, sampleRate);

            //Assert
            result.Spectrogram.Should().NotBeNull();
            result.FrequencyAxis.Should().NotBeEmpty();
            result.TimeAxis.Should().NotBeEmpty();
        }
    }
}