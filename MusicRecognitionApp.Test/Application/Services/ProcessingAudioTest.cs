using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Test.Application.Services
{
    public class ProcessingAudioTest
    {
        private readonly Mock<IAudioProcessor> _audioProcessorMock;
        private readonly Mock<ISpectrogramBuilder> _spectrogramBuilderMock;
        private readonly Mock<IPeakDetector> _peakDetectorMock;
        private readonly Mock<IAudioHashGenerator> _hashGeneratorMock;
        private readonly Mock<IServiceScopeFactory> _scopeFactoryMock;
        private readonly Mock<IServiceScope> _scopeMock;
        private readonly Mock<IServiceProvider> _providerMock;
        private readonly Mock<ISongImportService> _importServiceMock;
        private readonly ProcessingAudio _service;

        public ProcessingAudioTest()
        {
            _audioProcessorMock = new Mock<IAudioProcessor>();
            _spectrogramBuilderMock = new Mock<ISpectrogramBuilder>();
            _peakDetectorMock = new Mock<IPeakDetector>();
            _hashGeneratorMock = new Mock<IAudioHashGenerator>();

            _scopeFactoryMock = new Mock<IServiceScopeFactory>();
            _scopeMock = new Mock<IServiceScope>();
            _providerMock = new Mock<IServiceProvider>();
            _importServiceMock = new Mock<ISongImportService>();

            _scopeFactoryMock.Setup(f => f.CreateScope())
                .Returns(_scopeMock.Object);
            _scopeMock.Setup(s => s.ServiceProvider)
                .Returns(_providerMock.Object);
            _providerMock.Setup(p => p.GetService(typeof(ISongImportService)))
                .Returns(_importServiceMock.Object);

            _service = new ProcessingAudio(
                _audioProcessorMock.Object,
                _spectrogramBuilderMock.Object,
                _peakDetectorMock.Object,
                _hashGeneratorMock.Object,
                _scopeFactoryMock.Object);

            _audioProcessorMock.Setup(p => p.PreprocessAudio(It.IsAny<Stream>()))
                .Returns(new float[] { 1.0f });
            _spectrogramBuilderMock.Setup(s => s.ProcessAudio(It.IsAny<float[]>(), 11025))
                .Returns(new SpectrogramData());
            _peakDetectorMock.Setup(p => p.ProcessPeekDetector(It.IsAny<SpectrogramData>()))
                .Returns(new List<Peak>());
            _hashGeneratorMock.Setup(h => h.GenerateHashes(It.IsAny<List<Peak>>(), 0))
                .Returns(new List<AudioHash>());
            _importServiceMock.Setup(
                i => i.AddSongAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<List<AudioHash>>(), 
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }

        #region AddTracksFromFolderAsync Tests

        [Fact]
        public async Task AddTracksFromFolderAsync_FolderNotFound_ReturnsError()
        {
            // Arrange
            string invalidPath = "C:\\NonExistent";

            // Act
            var result = await _service.AddTracksFromFolderAsync(invalidPath);

            // Assert
            result.Added.Should().Be(0);
            result.Failed.Should().Be(0);
            result.Errors.Should().ContainSingle(e => e.Contains("Folder doesn't exist"));
            _importServiceMock.Verify(
                i => i.AddSongAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<List<AudioHash>>(), 
                    It.IsAny<CancellationToken>()), 
                Times.Never);
        }

        [Fact]
        public async Task AddTracksFromFolderAsync_NoAudioFiles_ReturnsError()
        {
            // Arrange
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            File.WriteAllText(Path.Combine(tempDir, "readme.txt"), "test");

            try
            {
                // Act
                var result = await _service.AddTracksFromFolderAsync(tempDir);

                // Assert
                result.Added.Should().Be(0);
                result.Errors.Should().ContainSingle(e => e.Contains("Audio files"));
                _importServiceMock.Verify(
                i => i.AddSongAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<List<AudioHash>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Fact]
        public async Task AddTracksFromFolderAsync_Success_ProcessesAllFilesAndCreatesScopes()
        {
            // Arrange
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            var subDir = Path.Combine(tempDir, "ArtistName");
            Directory.CreateDirectory(subDir);

            var file1 = Path.Combine(subDir, "Song1.mp3");
            var file2 = Path.Combine(subDir, "Song2.WAV");
            File.WriteAllBytes(file1, new byte[] { 0x00 });
            File.WriteAllBytes(file2, new byte[] { 0x00 });

            var progressSteps = new List<int>();
            _service.ImportProgress += (p) => progressSteps.Add(p);

            try
            {
                // Act
                var result = await _service.AddTracksFromFolderAsync(tempDir);

                // Assert
                result.Added.Should().Be(2);
                result.Failed.Should().Be(0);
                result.Errors.Should().BeEmpty();

                _importServiceMock.Verify(
                    i => i.AddSongAsync(
                        "Song1", 
                        "ArtistName", 
                        It.IsAny<List<AudioHash>>(), 
                        It.IsAny<CancellationToken>()), 
                    Times.Once);
                _importServiceMock.Verify(
                    i => i.AddSongAsync(
                        "Song2", 
                        "ArtistName", 
                        It.IsAny<List<AudioHash>>(), 
                        It.IsAny<CancellationToken>()), 
                    Times.Once);

                progressSteps.Should().ContainInOrder(new[] { 0, 50, 100 });
                _scopeFactoryMock.Verify(f => f.CreateScope(), 
                    Times.Exactly(2));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [Fact]
        public async Task AddTracksFromFolderAsync_PartialFailure_ContinuesAndCollectsErrors()
        {
            // Arrange
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            var file1 = Path.Combine(tempDir, "Bad.mp3");
            var file2 = Path.Combine(tempDir, "Good.mp3");
            File.WriteAllBytes(file1, new byte[] { 0x00 });
            File.WriteAllBytes(file2, new byte[] { 0x00 });

            _importServiceMock.SetupSequence(
                i => i.AddSongAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<List<AudioHash>>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("DB Error"))
                .Returns(Task.CompletedTask);

            try
            {
                // Act
                var result = await _service.AddTracksFromFolderAsync(tempDir);

                // Assert
                result.Added.Should().Be(1);
                result.Failed.Should().Be(1);
                result.Errors.Should().ContainSingle(e => e.Contains("Bad.mp3") && e.Contains("DB Error"));

                _importServiceMock.Verify(
                    i => i.AddSongAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<List<AudioHash>>(), 
                        It.IsAny<CancellationToken>()), 
                    Times.Exactly(2));
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        #endregion
    }
}