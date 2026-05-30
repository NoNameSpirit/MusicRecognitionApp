using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class DbSongService : IDbSongService
    {
        private readonly ISongRepository _songRepository;
        private readonly ILogger<DbSongService> _logger;

        public DbSongService(
            ISongRepository songRepository,
            ILogger<DbSongService> logger)
        {
            _songRepository = songRepository;
            _logger = logger;
        }

        public async Task<SongModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _songRepository.GetByIdAsync(id, cancellationToken);
                return entity == null ? null : EntityToModel.ToSongModel(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting song by ID {SongId}", id);
                return null;
            }
        }

        public async Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _songRepository.GetSongByTitleAndArtistAsync(title, artist, cancellationToken);

                return entity == null ? null : EntityToModel.ToSongModel(entity);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting song by title '{title}' and artist '{artist}'");
                return null;
            }
        }

        public async Task CreateAsync(string title, string artist,
            List<AudioHash> hashes, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be empty", nameof(title));

                if (string.IsNullOrWhiteSpace(artist))
                    throw new ArgumentException("Artist cannot be empty", nameof(artist));

                var songResult = await GetByTitleAndArtistAsync(title, artist, cancellationToken);
                if (songResult != null)
                {
                    _logger.LogInformation("Song already exists: '{Title}' by '{Artist}'", songResult.Title, songResult.Artist);
                    return;
                }

                if (hashes == null || hashes.Count == 0)
                {
                    _logger.LogWarning("No hashes to add for song Artist - Title: ", artist, title);
                    return;
                }

                var song = new SongEntity { Title = title, Artist = artist };
                foreach (AudioHash hash in hashes)
                {
                    song.AudioHashes.Add(new AudioHashEntity
                    {
                        Hash = hash.Hash,
                        TimeOffset = hash.TimeOffset,
                    });
                }

                await _songRepository.InsertAsync(song, cancellationToken);
                await _songRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating song '{title}' by '{artist}'");
                throw;
            }
        }
    }
}
