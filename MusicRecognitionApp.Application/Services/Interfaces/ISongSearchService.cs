using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface ISongSearchService
    {
        Task<List<SearchResult>> SearchSong(List<AudioHash> queryHashes, CancellationToken cancellationToken = default);
    }
}
