using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using System.Diagnostics;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface IAudioDatabase
    {
        List<SearchResultModel> SearchSong(List<AudioHash> queryHashes);

        Task SaveRecognizedSongsAsync(int songId, int matches);

        List<RecognizedSongModel> GetRecognizedSongs();
        
        List<ArtistStatisticModel> GetRecognizedArtists();

        Task AddSongWithHashesAsync(string title, string artist, List<AudioHash> hashes);

        bool TestConnection();
    }
}
