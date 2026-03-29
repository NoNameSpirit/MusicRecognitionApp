using FluentAssertions;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations;

namespace MusicRecognitionApp.Test.Infrastructure.Data.Repositories
{
    public class SongRepositoryTest
    {
        [Fact]
        public async Task GetSongByTitleAndArtistAsync_SameSong_CorrectSearch()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var songRepository = new SongRepository(context);

            var song = new SongEntity { Title = "Title", Artist = "Artist" };

            //Act
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var songEntity = await songRepository.GetSongByTitleAndArtistAsync(song.Title, song.Artist);

            //Assert
            songEntity.Should().NotBeNull();
            songEntity.Title.Should().Be(song.Title);
            songEntity.Artist.Should().Be(song.Artist);
        }

        [Fact]
        public async Task GetSongByTitleAndArtistAsync_OtherSong_IncorrectSearch()
        {
            //Arrange
            using var context = new TestDbFactory().Create();
            var songRepository = new SongRepository(context);

            var song = new SongEntity { Title = "Title", Artist = "Artist" };
            var otherSong = new SongEntity { Title = "Other Title", Artist = "Other Artist" };

            //Act
            await songRepository.InsertAsync(song);
            await context.SaveChangesAsync();

            var songEntity = await songRepository.GetSongByTitleAndArtistAsync(otherSong.Title, otherSong.Artist);

            //Assert
            songEntity.Should().BeNull();
        }
    }
}