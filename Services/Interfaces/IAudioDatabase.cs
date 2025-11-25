using MusicRecognitionApp.Data;

namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioDatabase
    {
        List<(int songId, string title, string artist, int matches, double confidence)> SearchSong(List<AudioHash> queryHashes);

        bool TestConnection();
    }
}
