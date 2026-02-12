using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly ILogger<SongService> _logger;

        public SongService(
            ISongRepository songRepository,
            ILogger<SongService> logger)
        {
            _songRepository = songRepository;
            _logger = logger;
        }

        public async Task<SongModel?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _songRepository.GetByIdAsync(id);
                return entity == null ? null : EntityToModel.ToSongModel(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting song by ID {SongId}", id);
                return null;
            }
        }

        public async Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist)
        {
            try
            {
                var entity = await _songRepository.GetSongByTitleAndArtistAsync(title, artist);

                return entity == null ? null : EntityToModel.ToSongModel(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting song by title '{Title}' and artist '{Artist}'", title, artist); 
                return null;
            }
        }

        public async Task<SongCreationResult> CreateAsync(string title, string artist, 
            List<AudioHash> hashes, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be empty", nameof(title));

                if (string.IsNullOrWhiteSpace(artist))
                    throw new ArgumentException("Artist cannot be empty", nameof(artist));

                var songResult = await GetByTitleAndArtistAsync(title, artist);
                if (songResult != null)
                {
                    _logger.LogInformation("Song already exists: '{Title}' by '{Artist}'", title, artist);
                    return new SongCreationResult(songResult, false);
                }

                if (hashes == null || hashes.Count == 0)
                {
                    _logger.LogWarning("No hashes to add for song Artist - Title: ", songResult.Artist, songResult.Title);
                    return new SongCreationResult(songResult, false);
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

                await _songRepository.InsertAsync(song);

                var model = EntityToModel.ToSongModel(song);
                return new SongCreationResult(model, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating song '{Title}' by '{Artist}'", title, artist); 
                throw;
            }
        }
    }
}
