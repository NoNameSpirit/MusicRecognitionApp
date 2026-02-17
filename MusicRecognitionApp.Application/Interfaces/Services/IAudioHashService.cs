namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IAudioHashService
    {
        Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues, CancellationToken cancellationToken = default);
    }
}
