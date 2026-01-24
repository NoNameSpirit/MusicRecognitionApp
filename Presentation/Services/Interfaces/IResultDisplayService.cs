using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IResultDisplayService
    {
        void ClearResults(Panel panelResults);

        Task DisplayResults(Panel panelResults, PictureBox picRecordingGif, List<SearchResultModel>? results);
    }
}
