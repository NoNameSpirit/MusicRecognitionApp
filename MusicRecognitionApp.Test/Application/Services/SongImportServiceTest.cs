using Microsoft.Extensions.Logging;
using Moq;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Test.Application.Services
{
    public class SongImportServiceTest
    {
        private readonly Mock<IDbSongService> _songServiceMock;
        private readonly Mock<ILogger<SongImportService>> _loggerMock;
        private readonly SongImportService _songImportService;
        public SongImportServiceTest()
        {
            _songServiceMock = new Mock<IDbSongService>();
            _loggerMock = new Mock<ILogger<SongImportService>>();
            _songImportService = new SongImportService(_songServiceMock.Object, _loggerMock.Object);
        }

        #region AddSongAsync Tests

        [Fact]
        public async Task AddSongAsync_NewSong_CallsSaveAsync()
        {
            // Arrange
            var hashes = new List<AudioHash> { new AudioHash(1, 0, 0) };
            var songModel = new SongModel() { Title = "New Song", Artist = "New Artist" };
            _songServiceMock.Setup(s => s.CreateAsync(songModel.Title, songModel.Artist, hashes, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _songImportService.AddSongAsync(songModel.Title, songModel.Artist, hashes);

            // Assert
            _songServiceMock.Verify(s => s.CreateAsync(songModel.Title, songModel.Artist, hashes, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddSongAsync_ExistingSong_DoesNotCallSaveAsync()
        {
            // Arrange
            var hashes = new List<AudioHash>();
            var songModel = new SongModel() { Title = "New Song", Artist = "New Artist" };
            _songServiceMock.Setup(s => s.CreateAsync(songModel.Title, songModel.Artist, hashes, It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

            // Act
            await _songImportService.AddSongAsync(songModel.Title, songModel.Artist, hashes);

            // Assert
            _songServiceMock.Verify(s => s.CreateAsync(songModel.Title, songModel.Artist, hashes, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddSongAsync_OperationCanceled_NotLogsAndRethrows()
        {
            // Arrange
            var title = "Song";
            var artist = "Artist";
            var hashes = new List<AudioHash>();
            _songServiceMock.Setup(s => s.CreateAsync(title, artist, hashes, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
                await _songImportService.AddSongAsync(title, artist, hashes));

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }

        [Fact]
        public async Task AddSongAsync_GeneralError_LogsAndRethrows()
        {
            // Arrange
            var title = "Song";
            var artist = "Artist";
            var hashes = new List<AudioHash>();
            var exception = new Exception("DB Connection lost");
            _songServiceMock.Setup(s => s.CreateAsync(title, artist, hashes, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await _songImportService.AddSongAsync(title, artist, hashes));

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Exception while adding track")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion
    }
}
