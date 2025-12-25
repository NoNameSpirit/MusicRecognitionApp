using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Data.Interfaces;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
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

        public Dictionary<uint, List<AudioHash>> GetHashesDictionary(List<uint> hashValues)
        {
            try
            {
                var matchingHashes = _audioHashRepository.GetByHashes(hashValues, "Song");

                var convertedHashes = matchingHashes
                    .Select(EntityToModel.ToAudioHash)
                    .ToList();

                return convertedHashes
                    .GroupBy(h => h.Hash)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while getting hashes dictionary: {ex.Message}");
                return new Dictionary<uint, List<AudioHash>>();
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
                    _audioHashRepository.InsertAsync(entity);
                }

                _audioHashRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while adding hashes for song {songId}: {ex.Message}");
                throw;
            }
        }
    }
}
