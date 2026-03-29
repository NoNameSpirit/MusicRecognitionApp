using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Implementations;

namespace MusicRecognitionApp.Test.Services
{
    public class AudioHashServiceTest
    {
        private readonly Mock<IAudioHashRepository> _repoMock;
        private readonly Mock<ILogger<AudioHashService>> _loggerMock;
        private readonly AudioHashService _service;

        public AudioHashServiceTest()
        {
            _repoMock = new Mock<IAudioHashRepository>();
            _loggerMock = new Mock<ILogger<AudioHashService>>();
            _service = new AudioHashService(_repoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task FindSongMatchesAsync_FoundedMatches_NotEmptyResult()
        {
            // Arrange
            var hashes = new List<uint> { 1, 2, 3 };
            var expectedMatches = new List<(int SongId, int Count)> { (1, 5), (2, 3) };

            _repoMock.Setup(r => r.GetMatchesAsync(hashes, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(expectedMatches);

            // Act
            var result = await _service.FindSongMatchesAsync(hashes);

            // Assert
            result.Should().BeEquivalentTo(expectedMatches);
        }

        [Fact]
        public async Task FindSongMatchesAsync_GeneralError_LogsAndReturnsEmptyList()
        {
            // Arrange
            var hashes = new List<uint> { 1, 2 };
            _repoMock.Setup(r => r.GetMatchesAsync(hashes, It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new Exception("Exception"));

            // Act
            var result = await _service.FindSongMatchesAsync(hashes);

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Error while getting song matches")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task FindSongMatchesAsync_OperationCanceled_ThrowsWithoutLogging()
        {
            // Arrange
            var hashes = new List<uint> { 1, 2 };
            _repoMock.Setup(r => r.GetMatchesAsync(hashes, It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new OperationCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
                await _service.FindSongMatchesAsync(hashes));

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error, 
                    It.IsAny<EventId>(), 
                    It.IsAny<It.IsAnyType>(), 
                    It.IsAny<Exception>(), 
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }
    }
}