using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using System.Diagnostics;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class AudioHashService : IAudioHashService
    {
        private readonly IAudioHashRepository _audioHashRepository;

        public AudioHashService(IAudioHashRepository audioHashRepository)
        {
            _audioHashRepository = audioHashRepository;
        }

        public async Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues)
        {
            try
            {
                return await _audioHashRepository.GetMatchesAsync(hashValues);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while getting hashes: {ex.Message}");
                return new List<(int, int)>();
            }
        }

        public async Task AddHashesAsync(List<AudioHash> hashes, int songId)
        {
            try
            {
                if (hashes == null || hashes.Count == 0)
                {
                    Debug.WriteLine($"[WARN] No hashes to add for song {songId}");
                    return;
                }

                foreach (AudioHash hash in hashes)
                {
                    var entity = EntityToModel.ToAudioHashEntity(hash, songId);
                    await _audioHashRepository.InsertAsync(entity);
                }

                await _audioHashRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while adding hashes for song {songId}: {ex.Message}");
                throw;
            }
        }
    }
}
