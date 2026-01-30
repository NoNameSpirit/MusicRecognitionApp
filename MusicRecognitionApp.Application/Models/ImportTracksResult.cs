namespace MusicRecognitionApp.Application.Models
{
    public record ImportTracksResult(int Added, int Failed, List<string> Errors);
}
