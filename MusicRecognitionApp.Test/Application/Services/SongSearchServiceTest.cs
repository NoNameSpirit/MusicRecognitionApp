using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Test.Application.Services
{
    public class SongSearchServiceTest
    {
        private readonly Mock<ISongService> _songServiceMock;
        private readonly Mock<IAudioHashService> _audioHashServiceMock;
        private readonly Mock<ILogger<SongSearchService>> _loggerMock;
        private readonly SongSearchService _songSearchService;
        public SongSearchServiceTest()
        {
            _audioHashServiceMock = new Mock<IAudioHashService>();
            _songServiceMock = new Mock<ISongService>();
            _loggerMock = new Mock<ILogger<SongSearchService>>();
            _songSearchService = new SongSearchService(_audioHashServiceMock.Object, _songServiceMock.Object, _loggerMock.Object);
        }

        #region SearchSong Tests

        [Fact]
        public async Task SearchSong_EmptyHashes_EmpltyResult()
        {
            //Arrange
            var emptyHashes = new List<AudioHash>();

            //Act
            var result = await _songSearchService.SearchSongAsync(emptyHashes);

            //Assert
            result.Should().BeEmpty();
            _audioHashServiceMock.Verify(a
                => a.FindSongMatchesAsync(
                    It.IsAny<List<uint>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task SearchSong_FoundMatches_CalculatesConfidenceAndSorts()
        {
            // Arrange
            var queryHashes = new List<AudioHash>
            {
                new AudioHash(100, 0, 0),
                new AudioHash(200, 0, 0),
                new AudioHash(300, 0, 0)
            };
            var matchesFromRepo = new List<(int SongId, int Count)>
            {
                (1, 3),
                (2, 1)
            };
            _audioHashServiceMock.Setup(a => a.FindSongMatchesAsync(It.IsAny<List<uint>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(matchesFromRepo);

            var song1 = new SongModel
            {
                Id = 1,
                Title = "Song One",
                Artist = "Artist A"
            };
            var song2 = new SongModel
            {
                Id = 2,
                Title = "Song Two",
                Artist = "Artist B"
            };
            _songServiceMock.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(song1);
            _songServiceMock.Setup(s => s.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(song2);

            // Act
            var result = await _songSearchService.SearchSongAsync(queryHashes);

            // Assert
            result.Should().HaveCount(2);

            var topResult = result.First();
            topResult.Song.Id.Should().Be(1);
            topResult.Confidence.Should().BeApproximately(1.0, 0.01);
            topResult.Matches.Should().Be(3);

            var secondResult = result.Last();
            secondResult.Song.Id.Should().Be(2);
            secondResult.Confidence.Should().BeApproximately(0.33, 0.01);
            secondResult.Matches.Should().Be(1);

            (result[0].Confidence >= result[1].Confidence).Should().BeTrue();
        }

        [Fact]
        public async Task SearchSong_SongNotFoundById_SkipsMissingSong()
        {
            // Arrange
            var queryHashes = new List<AudioHash> 
            { 
                new AudioHash(100, 0, 0) 
            };
            var matchesFromRepo = new List<(int SongId, int Count)> 
            { 
                (999, 1) 
            };

            _audioHashServiceMock.Setup(a => a.FindSongMatchesAsync(It.IsAny<List<uint>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(matchesFromRepo);
            _songServiceMock.Setup(s => s.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((SongModel?)null);

            // Act
            var result = await _songSearchService.SearchSongAsync(queryHashes);

            // Assert
            result.Should().BeEmpty();
            _songServiceMock.Verify(s 
                => s.GetByIdAsync(
                    999, 
                    It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task SearchSong_GeneralError_EmptyListAndLogs()
        {
            // Arrange
            var queryHashes = new List<AudioHash> 
            { 
                new AudioHash(100, 0, 0) 
            };

            _audioHashServiceMock.Setup(a => a.FindSongMatchesAsync(It.IsAny<List<uint>>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Search Failure"));

            // Act
            var result = await _songSearchService.SearchSongAsync(queryHashes);

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error, 
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, c) => v.ToString().Contains("Exception while searching a song")),
                    It.IsAny<Exception>(), 
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task SearchSong_OperationCanceled_ThrowsWithoutLogging()
        {
            // Arrange
            var queryHashes = new List<AudioHash> 
            { 
                new AudioHash(100, 0, 0) 
            };
            _audioHashServiceMock.Setup(a => a.FindSongMatchesAsync(It.IsAny<List<uint>>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
                await _songSearchService.SearchSongAsync(queryHashes));

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error, 
                    It.IsAny<EventId>(), 
                    It.IsAny<It.IsAnyType>(), 
                    It.IsAny<Exception>(), 
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        } 

        #endregion
    }
}
