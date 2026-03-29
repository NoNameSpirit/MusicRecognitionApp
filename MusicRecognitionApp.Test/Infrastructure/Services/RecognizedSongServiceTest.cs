using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MusicRecognitionApp.Application.Interfaces.UnitOfWork;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Services
{
    public class RecognizedSongServiceTest
    {
        private readonly Mock<IRecognizedSongRepository> _repoMock;
        private readonly Mock<ILogger<RecognizedSongService>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RecognizedSongService _recognizedSongService;

        public RecognizedSongServiceTest()
        {
            _repoMock = new Mock<IRecognizedSongRepository>();
            _loggerMock = new Mock<ILogger<RecognizedSongService>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
             _recognizedSongService = new RecognizedSongService(_repoMock.Object, _loggerMock.Object, _unitOfWorkMock.Object);
        }

        #region SaveRecognizedSongAsync Tests

        [Fact]
        public async Task SaveRecognizedSongAsync_MatchesLessThanOne_NotSavedAndLogged()
        {
            //Arrange
            int songId = 1;
            int matches = 0;

            //Act
            await _recognizedSongService.SaveRecognizedSongAsync(songId, matches);

            //Assert
            _repoMock.Verify(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o,t) => o.ToString().Contains("Skipping save")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task SaveRecognizedSongAsync_ValidData_SuccessfulSave()
        {
            //Arrange
            int songId = 1;
            int matches = 5;

            _repoMock.Setup(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            //Act
            await _recognizedSongService.SaveRecognizedSongAsync(songId, matches);

            //Assert
            _repoMock.Verify(r 
                => r.InsertAsync(
                    It.Is<RecognizedSongEntity>(r => r.SongId == songId && r.Matches == matches),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Clear(), Times.Never);
        }

        [Fact]
        public async Task SaveRecognizedSongAsync_ValidData_CanceledSaveWhileInserting()
        {
            //Arrange
            int songId = 1;
            int matches = 5;

            _repoMock.Setup(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            //Act
            await Assert.ThrowsAsync<OperationCanceledException>(async () 
                => await _recognizedSongService.SaveRecognizedSongAsync(songId, matches));

            //Assert
            _repoMock.Verify(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Clear(), Times.Once);
        }

        [Fact]
        public async Task SaveRecognizedSongAsync_ValidData_CanceledSaveWhileSaving()
        {
            //Arrange
            int songId = 1;
            int matches = 5;

            _repoMock.Setup(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            //Act
            await Assert.ThrowsAsync<OperationCanceledException>(async ()
                => await _recognizedSongService.SaveRecognizedSongAsync(songId, matches));

            //Assert
            _repoMock.Verify(r => r.InsertAsync(It.IsAny<RecognizedSongEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Clear(), Times.Once);
        }

        #endregion

        #region GetRecognizedSongsAsync Tests

        [Fact]
        public async Task GetRecognizedSongsAsync_ExistsSongsInDb_MappedModels()
        {
            // Arrange
            var entities = new List<RecognizedSongEntity>
            {
                new() { 
                    Id = 1, 
                    SongId = 10, 
                    Matches = 5, 
                    RecognitionDate = DateTime.UtcNow, 
                    Song = new() { Title = "Song1", Artist = "Artist1" } 
                }
            };
            _repoMock.Setup(r => r.GetAllOrderedByDateAsync())
                .ReturnsAsync(entities);

            // Act
            var result = await _recognizedSongService.GetRecognizedSongsAsync();

            // Assert
            result.Should().HaveCount(1);
            result[0].Song.Title.Should().Be("Song1");
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
        public async Task GetRecognizedSongsAsync_Exception_EmptyListAndLogs()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAllOrderedByDateAsync()).ThrowsAsync(new Exception("DB Error"));

            // Act
            var result = await _recognizedSongService.GetRecognizedSongsAsync();

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(
                l => l.Log(LogLevel.Error, It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Error retrieving recognized songs")),
                    It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion

        #region GetArtistsStatisticsAsync Tests

        [Fact]
        public async Task GetArtistsStatisticsAsync_ExistsStatsInDb_Stats()
        {
            // Arrange
            var stats = new List<ArtistStatisticModel> 
            {
                new() { 
                    Artist = "Queen", 
                    SongCount = 10 
                } 
            };
            _repoMock.Setup(r => r.GetArtistsStatisticsAsync())
                .ReturnsAsync(stats);

            // Act
            var result = await _recognizedSongService.GetArtistsStatisticsAsync();

            // Assert
            result.Should().HaveCount(1);
            result[0].Artist.Should().Be("Queen");
        }

        [Fact]
        public async Task GetArtistsStatisticsAsync_Exception_EmptyListAndLogs()
        {
            // Arrange
            _repoMock.Setup(r => r.GetArtistsStatisticsAsync())
                .ThrowsAsync(new Exception("DB Error"));

            // Act
            var result = await _recognizedSongService.GetArtistsStatisticsAsync();

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error, 
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Error retrieving artist statistics")),
                    It.IsAny<Exception>(), 
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion
    }
}