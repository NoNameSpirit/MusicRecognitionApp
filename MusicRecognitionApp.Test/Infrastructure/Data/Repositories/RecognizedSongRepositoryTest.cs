using FluentAssertions;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Data.Repositories
{
    public class RecognizedSongRepositoryTest
    {
        [Fact]
        public async Task GetAllOrderedByDateAsync_SortedEntitiesByDesc_FirstThenSecondEntity()
        {
            //Arrange
            using var context = new TestDbFactory().Create();

            var song = new SongEntity
            {
                Title = "Test Song",
                Artist = "Test Artist"
            };
            var songRepository = new SongRepository(context);
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var fisrtEntity = new RecognizedSongEntity()
            {
                SongId = song.Id,
                RecognitionDate = new DateTime(2023, 1, 1, 12, 0, 0),
            };
            var secondEntity = new RecognizedSongEntity()
            {
                SongId = song.Id,
                RecognitionDate = new DateTime(2023, 1, 1, 10, 0, 0)
            };

            var recognizedSongRepository = new RecognizedSongRepository(context);
            await recognizedSongRepository.InsertAsync(fisrtEntity);
            await context.SaveChangesAsync();
            await recognizedSongRepository.InsertAsync(secondEntity);
            await context.SaveChangesAsync();

            //Act
            var result = await recognizedSongRepository.GetAllOrderedByDateAsync();

            //Assert
            result.Should().HaveCount(2);
            (result[0].RecognitionDate >= result[1].RecognitionDate).Should().BeTrue();
        }

        [Fact]
        public async Task GetAllOrderedByDateAsync_NoEnities_EmptyResult()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var recognizedSongRepository = new RecognizedSongRepository(context);

            //Act
            var result = await recognizedSongRepository.GetAllOrderedByDateAsync();

            //Assert
            result.Should().BeEmpty();
        }



        [Fact]
        public async Task GetRecentAsync_SortedEntitiesByDesc_FirstThenSecondEntity()
        {
            //Arrange
            using var context = new TestDbFactory().Create();

            var song = new SongEntity
            {
                Title = "Test Song",
                Artist = "Test Artist"
            };
            var songRepository = new SongRepository(context);
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var fisrtEntity = new RecognizedSongEntity()
            {
                SongId = song.Id,
                RecognitionDate = new DateTime(2023, 1, 1, 12, 0, 0),
            };
            var secondEntity = new RecognizedSongEntity()
            {
                SongId = song.Id,
                RecognitionDate = new DateTime(2023, 1, 1, 10, 0, 0)
            };

            var recognizedSongRepository = new RecognizedSongRepository(context);
            await recognizedSongRepository.InsertAsync(fisrtEntity);
            await context.SaveChangesAsync();
            await recognizedSongRepository.InsertAsync(secondEntity);
            await context.SaveChangesAsync();

            //Act
            var result = await recognizedSongRepository.GetRecentAsync();

            //Assert
            result.Should().HaveCount(2);
            (result[0].RecognitionDate >= result[1].RecognitionDate).Should().BeTrue();
        }

        [Fact]
        public async Task GetRecentAsync_NoEnities_EmptyResult()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var recognizedSongRepository = new RecognizedSongRepository(context);

            //Act
            var result = await recognizedSongRepository.GetRecentAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetArtistsStatisticsAsync_GroupsCorrectly_CorrectSongAmountAndArtist()
        {
            // Arrange
            using var context = new TestDbFactory().Create();
            var repo = new RecognizedSongRepository(context);
            var songRepo = new SongRepository(context);

            var songQueen1 = new SongEntity { Title = "First Title", Artist = "Queen" };
            var songQueen2 = new SongEntity { Title = "Second Title", Artist = "Queen" };
            var songEagles = new SongEntity { Title = "Third Title", Artist = "Eagles" };

            await songRepo.InsertAsync(songQueen1);
            await songRepo.InsertAsync(songQueen2);
            await songRepo.InsertAsync(songEagles);
            await context.SaveChangesAsync();

            await repo.InsertAsync(new RecognizedSongEntity { SongId = songQueen1.Id, Matches = 5 });
            await repo.InsertAsync(new RecognizedSongEntity { SongId = songQueen2.Id, Matches = 3 });
            await repo.InsertAsync(new RecognizedSongEntity { SongId = songEagles.Id, Matches = 8 });
            await context.SaveChangesAsync();

            // Act
            var stats = await repo.GetArtistsStatisticsAsync();

            // Assert
            stats.Should().HaveCount(2);

            var queenStat = stats.FirstOrDefault(s => s.Artist == "Queen");
            queenStat.Should().NotBeNull();
            queenStat!.SongCount.Should().Be(2);

            var eaglesStat = stats.FirstOrDefault(s => s.Artist == "Eagles");
            eaglesStat.Should().NotBeNull();
            eaglesStat!.SongCount.Should().Be(1);
        }
    }
}
