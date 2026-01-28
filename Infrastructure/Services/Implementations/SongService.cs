using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
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

        public async Task<SongModel> CreateAsync(string title, string artist)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be empty", nameof(title));

                if (string.IsNullOrWhiteSpace(artist))
                    throw new ArgumentException("Artist cannot be empty", nameof(artist));

                var existing = await GetByTitleAndArtistAsync(title, artist);
                if (existing != null)
                {
                    _logger.LogInformation("Song already exists: '{Title}' by '{Artist}'", title, artist); 
                    return existing;
                }

                var entity = new SongEntity
                {
                    Title = title,
                    Artist = artist
                };

                await _songRepository.InsertAsync(entity);
                await _songRepository.SaveChangesAsync();

                var created = await _songRepository.GetSongByTitleAndArtistAsync(title, artist);
                return EntityToModel.ToSongModel(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating song '{Title}' by '{Artist}'", title, artist); 
                throw;
            }
        }
    }
}
