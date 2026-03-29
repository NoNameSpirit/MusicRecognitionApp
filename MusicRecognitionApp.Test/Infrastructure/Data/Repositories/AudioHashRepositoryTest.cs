using FluentAssertions;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Data.Repositories
{
    public class AudioHashRepositoryTest
    {

        [Fact]
        public async Task GetMatchesAsync_CorrectQueryHashes_MatchesFound()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var audioHashRepository = new AudioHashRepository(context);
            var songRepository = new SongRepository(context);

            var song = new SongEntity { Title = "Test Song", Artist = "Test Artist" };
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var queryHashes = new List<uint> { 1, 2 };
            for (int i = 0; i < queryHashes.Count; i++)
            {
                var audioHashEntity = new AudioHashEntity { Hash = queryHashes[i], SongId = song.Id };

                await audioHashRepository.InsertAsync(audioHashEntity);
            }
            await context.SaveChangesAsync();

            //Act
            var result = await audioHashRepository.GetMatchesAsync(queryHashes);

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetMatchesAsync_InCorrectQueryHashes_MatchesNotFound()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var audioHashRepository = new AudioHashRepository(context);
            var songRepository = new SongRepository(context);

            var song = new SongEntity { Title = "Test Song", Artist = "Test Artist" };
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var queryHashes = new List<uint> { 1, 2 };
            for (int i = 0; i < queryHashes.Count; i++)
            {
                var audioHashEntity = new AudioHashEntity { Hash = queryHashes[i], SongId = song.Id };

                await audioHashRepository.InsertAsync(audioHashEntity);
            }
            await context.SaveChangesAsync();

            //Act
            var otherHashes = new List<uint> { 3, 4 };

            var result = await audioHashRepository.GetMatchesAsync(otherHashes);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetMatchesAsync_NotEnoughHashes_MatchesNotFound()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var audioHashRepository = new AudioHashRepository(context);
            var songRepository = new SongRepository(context);

            var song = new SongEntity { Title = "Test Song", Artist = "Test Artist" };
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var queryHashes = new List<uint> { 1 };
            for (int i = 0; i < queryHashes.Count; i++)
            {
                var audioHashEntity = new AudioHashEntity { Hash = queryHashes[i], SongId = song.Id };

                await audioHashRepository.InsertAsync(audioHashEntity);
            }
            await context.SaveChangesAsync();

            //Act
            var result = await audioHashRepository.GetMatchesAsync(queryHashes);

            //Assert
            result.Should().BeEmpty();
        }
    }
}