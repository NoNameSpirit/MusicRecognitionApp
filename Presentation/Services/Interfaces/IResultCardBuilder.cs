using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IResultCardBuilder
    {
        MaterialCard CreateResultCard(SearchResultModel result);

        MaterialCard CreateNoResultsCard();
    }
}