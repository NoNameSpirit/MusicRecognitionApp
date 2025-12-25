using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Services.Search
{
    public interface ISongSearchService
    {
        List<SearchResultModel> SearchSong(List<AudioHash> queryHashes);
    }
}
