using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using System.Diagnostics;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
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
                Debug.WriteLine($"[ERROR] Exception while getting song by id {id}: {ex.Message}");
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
                Debug.WriteLine($"[ERROR] Exception while getting song: title - {title}, artist - {artist}, message: {ex.Message}");
                return null;
            }
        }

        public async Task<SongModel> CreateAsync(string title, string artist)
        {
            try
            {
                var existing = await GetByTitleAndArtistAsync(title, artist);
                if (existing != null)
                {
                    Debug.WriteLine($"[INFO] Song already exists: {title} - {artist}");
                    return existing;
                }

                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Title cannot be empty", nameof(title));

                if (string.IsNullOrWhiteSpace(artist))
                    throw new ArgumentException("Artist cannot be empty", nameof(artist));

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
                Debug.WriteLine($"[ERROR] Exception while creating song: {ex.Message}");
                throw;
            }
        }
    }
}
