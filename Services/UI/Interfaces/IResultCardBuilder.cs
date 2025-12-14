using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Services.UI.Interfaces
{
    public interface IResultCardBuilder
    {
        MaterialCard CreateResultCard(SearchResultModel result);
        
        MaterialCard CreateNoResultsCard();
    }
}