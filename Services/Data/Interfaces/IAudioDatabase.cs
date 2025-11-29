using MusicRecognitionApp.Data;

namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioDatabase
    {
        List<(int songId, string title, string artist, int matches, double confidence)> SearchSong(List<AudioHash> queryHashes);

        bool TestConnection();

        void SaveRecognizedSongs(int songId, string title, string artist, int matches);

        List<(int songId, string title, string artist, int matches, DateTime recognitionDate)> GetRecognizedSongs();
        
        List<(string artist, int songCount)> GetRecognizedArtists();
    }
}
