using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IDbAudioHashService
    {
        Task<List<SongMatchDto>> FindSongMatchesAsync(List<uint> hashValues, CancellationToken cancellationToken = default);
    }
}
