using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Models
{
    public record SongCreationResult(SongModel Song, bool IsNew);
}
