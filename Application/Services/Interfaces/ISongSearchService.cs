using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface ISongSearchService
    {
        List<SearchResultModel> SearchSong(List<AudioHash> queryHashes);
    }
}
