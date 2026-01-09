using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface ISongSearchService
    {
        Task<List<SearchResultModel>> SearchSong(List<AudioHash> queryHashes);
    }
}
