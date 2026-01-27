using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Models
{
    public record SearchResult(SongModel Song, int Matches, double Confidence);
}
