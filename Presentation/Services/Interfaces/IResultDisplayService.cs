using MusicRecognitionApp.Application.Models;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IResultDisplayService
    {
        void ClearResults(Panel panelResults);

        Task DisplayResults(Panel panelResults, PictureBox picRecordingGif, List<SearchResult>? results);
    }
}
