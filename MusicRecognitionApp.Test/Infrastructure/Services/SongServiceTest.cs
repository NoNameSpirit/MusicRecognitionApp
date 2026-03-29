using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Services
{
    public class SongServiceTest
    {
        private readonly Mock<ISongRepository> _repoMock;
        private readonly Mock<ILogger<SongService>> _loggerMock;
        private readonly SongService _service;

        public SongServiceTest()
        {
            _repoMock = new Mock<ISongRepository>();
            _loggerMock = new Mock<ILogger<SongService>>();
            _service = new SongService(_repoMock.Object, _loggerMock.Object);
        }

        #region GetByIdAsync Tests

        [Fact]
        public async Task GetByIdAsync_NotFoundById_Null()
        {
            //Arrange
            var id = 1;
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SongEntity?)null);

            //Act
            var result = await _service.GetByIdAsync(id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ExistsEntity_FoundById()
        {
            //Arrange
            var entity = new SongEntity { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _service.GetByIdAsync(entity.Id);

            //Assert
            _repoMock.Verify(r 
                => r.GetByIdAsync(
                    1, 
                    It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Exception_NullAndLogError()
        {
            //Arrange
            var entity = new SongEntity { Id = 1 };
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            //Act
            var result = await _service.GetByIdAsync(entity.Id);

            //Assert
            result.Should().BeNull();
            _repoMock.Verify(r
                => r.GetByIdAsync(
                    1,
                    It.IsAny<CancellationToken>()),
                Times.Once);
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error, 
                    It.IsAny<EventId>(), 
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Error getting song by ID")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion

        #region GetByTitleAndArtistAsync Tests

        [Fact]
        public async Task GetByTitleAndArtistAsync_NotFoundById_Null()
        {
            //Arrange
            var title = "Title";
            var artist = "Artist";
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((SongEntity?)null);

            //Act
            var result = await _service.GetByTitleAndArtistAsync(title, artist);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByTitleAndArtistAsync_ExistsEntity_FoundById()
        {
            //Arrange
            var entity = new SongEntity { Title = "Title", Artist = "Artist" };
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _service.GetByTitleAndArtistAsync(entity.Title, entity.Artist);

            //Assert
            _repoMock.Verify(r
                => r.GetSongByTitleAndArtistAsync(
                    entity.Title, 
                    entity.Artist),
                Times.Once);
        }

        [Fact]
        public async Task GetByTitleAndArtistAsync_Exception_NullAndLogError()
        {
            //Arrange
            var entity = new SongEntity { Title = "Title", Artist = "Artist" };
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            //Act
            var result = await _service.GetByTitleAndArtistAsync(entity.Title, entity.Artist);

            //Assert
            result.Should().BeNull();
            _repoMock.Verify(r
                => r.GetSongByTitleAndArtistAsync(
                    entity.Title,
                    entity.Artist),
                Times.Once);
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Error getting song by title")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        public async Task CreateAsync_EmptyTitle_ThrowsException()
        {
            //Arrange
            var title = "";
            var artist = "Artist";
            var hashes = new List<AudioHash>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _service.CreateAsync(title, artist, hashes));
        }

        [Fact]
        public async Task CreateAsync_EmptyArtist_ThrowsException()
        {
            //Arrange
            var title = "Title";
            var artist = "";
            var hashes = new List<AudioHash>();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _service.CreateAsync(title, artist, hashes));
        }

        [Fact]
        public async Task CreateAsync_SongExists_ExistingWithIsNewFalse()
        {
            // Arrange
            var entity = new SongEntity { Title = "Title", Artist = "Artist" };
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(entity.Title, entity.Artist))
                     .ReturnsAsync(entity);

            // Act
            var result = await _service.CreateAsync(entity.Title, entity.Artist, new List<AudioHash>());

            // Assert
            result.IsNew.Should().BeFalse();
            _repoMock.Verify(r => r.InsertAsync(It.IsAny<SongEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_NoHashes_LogsWarningAndReturnsFalse()
        {
            // Arrange
            var entity = new SongEntity { Title = "Title", Artist = "Artist" };
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync((SongEntity?)null);

            // Act
            var result = await _service.CreateAsync(entity.Title, entity.Artist, new List<AudioHash>());

            // Assert
            result.IsNew.Should().BeFalse();
            _repoMock.Verify(r => r.InsertAsync(It.IsAny<SongEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("No hashes to add for song Artist - Title:")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ValidData_CreatesSongAndReturnsTrue()
        {
            // Arrange
            var entity = new SongEntity { Title = "Title", Artist = "Artist" };
            _repoMock.Setup(r => r.GetSongByTitleAndArtistAsync(It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync((SongEntity?)null);

            var hashes = new List<AudioHash> { new AudioHash(123, 1.5, 0) };

            // Act
            var result = await _service.CreateAsync(entity.Title, entity.Artist, hashes);

            // Assert
            result.IsNew.Should().BeTrue();
            _repoMock.Verify(r => r.InsertAsync(
                It.Is<SongEntity>(s => 
                    s.Title == entity.Title && 
                    s.Artist == entity.Artist && 
                    s.AudioHashes.Count == 1),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        #endregion
    }
}
