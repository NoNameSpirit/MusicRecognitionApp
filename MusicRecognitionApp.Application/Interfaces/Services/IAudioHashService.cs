using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IAudioHashService
    {
        Task<List<SongMatchDto>> FindSongMatchesAsync(List<uint> hashValues, CancellationToken cancellationToken = default);
    }
}
