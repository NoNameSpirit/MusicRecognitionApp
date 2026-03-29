using FluentAssertions;
using Moq;
using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Test.Application.Services
{
    public class RecognitionServiceTest
    {
        private readonly Mock<IAudioProcessor> _audioProcessorMock;
        private readonly Mock<ISpectrogramBuilder> _spectrogramBuilderMock;
        private readonly Mock<IPeakDetector> _peakDetectorMock;
        private readonly Mock<IAudioHashGenerator> _hashGeneratorMock;
        private readonly Mock<ISongSearchService> _searchServiceMock;
        private readonly RecognitionService _recognitionService;

        private const int TargetSampleRate = 11025;

        public RecognitionServiceTest()
        {
            _audioProcessorMock = new Mock<IAudioProcessor>();
            _spectrogramBuilderMock = new Mock<ISpectrogramBuilder>();
            _peakDetectorMock = new Mock<IPeakDetector>();
            _hashGeneratorMock = new Mock<IAudioHashGenerator>();
            _searchServiceMock = new Mock<ISongSearchService>();

            _recognitionService = new RecognitionService(
                _audioProcessorMock.Object,
                _spectrogramBuilderMock.Object,
                _peakDetectorMock.Object,
                _hashGeneratorMock.Object,
                _searchServiceMock.Object);
        }

        #region RecognizeFromMicrophoneAsync Tests

        [Fact]
        public async Task RecognizeFromMicrophoneAsync_EmptyPath_ReturnsEmptyList()
        {
            // Arrange
            string emptyPath = "";

            // Act
            var result = await _recognitionService.RecognizeFromMicrophoneAsync(emptyPath);

            // Assert
            result.Should().BeEmpty();
            _audioProcessorMock.Verify(p => p.PreprocessAudio(It.IsAny<Stream>()), 
                Times.Never);
            _searchServiceMock.Verify(s => s.SearchSongAsync(It.IsAny<List<AudioHash>>(), It.IsAny<CancellationToken>()), 
                Times.Never);
        }

        [Fact]
        public async Task RecognizeFromMicrophoneAsync_Success_CallsAllStepsAndReturnsResults()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, new byte[] { 0x00 });

            var processedAudio = new float[] { 1.0f, 2.0f };
            var spectrogramData = new SpectrogramData();
            var peaks = new List<Peak> 
            { 
                new Peak 
                { 
                    TimeIndex = 1, 
                    FrequencyIndex = 2, 
                    TimeSeconds = 0.1, 
                    FrequencyHz = 440, 
                    Magnitude = 1.0 
                } 
            };
            var hashes = new List<AudioHash> 
            { 
                new AudioHash(123, 0.1, 0) 
            };
            var searchResults = new List<SearchResult> 
            { 
                new SearchResult(new SongModel(), 1, 0.9) 
            };

            _audioProcessorMock.Setup(p => p.PreprocessAudio(It.IsAny<Stream>()))
                .Returns(processedAudio);
            _spectrogramBuilderMock.Setup(s => s.ProcessAudio(processedAudio, TargetSampleRate))
                .Returns(spectrogramData);
            _peakDetectorMock.Setup(p => p.ProcessPeekDetector(spectrogramData))
                .Returns(peaks);
            _hashGeneratorMock.Setup(h => h.GenerateHashes(peaks, 0))
                .Returns(hashes);
            _searchServiceMock.Setup(s => s.SearchSongAsync(hashes, It.IsAny<CancellationToken>()))
                .ReturnsAsync(searchResults);

            try
            {
                // Act
                var result = await _recognitionService.RecognizeFromMicrophoneAsync(tempFile);

                // Assert
                result.Should().HaveCount(1);
                result.Should().BeEquivalentTo(searchResults);

                _audioProcessorMock.Verify(p => p.PreprocessAudio(It.IsAny<Stream>()), 
                    Times.Once);
                _spectrogramBuilderMock.Verify(s => s.ProcessAudio(processedAudio, TargetSampleRate), 
                    Times.Once);
                _peakDetectorMock.Verify(p => p.ProcessPeekDetector(spectrogramData), 
                    Times.Once);
                _hashGeneratorMock.Verify(h => h.GenerateHashes(peaks, 0), 
                    Times.Once);
                _searchServiceMock.Verify(s => s.SearchSongAsync(hashes, It.IsAny<CancellationToken>()), 
                    Times.Once);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
            }
        }

        [Fact]
        public async Task RecognizeFromMicrophoneAsync_ErrorInProcessing_StopsPipelineAndThrows()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, new byte[] { 0x00 });

            _audioProcessorMock.Setup(p => p.PreprocessAudio(It.IsAny<Stream>()))
                .Returns(new float[] { 1.0f });
            _spectrogramBuilderMock.Setup(s => s.ProcessAudio(It.IsAny<float[]>(), It.IsAny<int>()))
                .Throws(new Exception("FFT Error"));
            
            try
            {
                // Act & Assert
                await Assert.ThrowsAsync<Exception>(async () =>
                    await _recognitionService.RecognizeFromMicrophoneAsync(tempFile));

                _peakDetectorMock.Verify(p => p.ProcessPeekDetector(It.IsAny<SpectrogramData>()), 
                    Times.Never);
                _hashGeneratorMock.Verify(h => h.GenerateHashes(It.IsAny<List<Peak>>(), It.IsAny<int>()), 
                    Times.Never);
                _searchServiceMock.Verify(s => s.SearchSongAsync(It.IsAny<List<AudioHash>>(), It.IsAny<CancellationToken>()), 
                    Times.Never);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
            }
        }

        #endregion
    }
}