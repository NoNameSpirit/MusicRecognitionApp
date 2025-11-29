using MaterialSkin.Controls;

namespace MusicRecognitionApp.Services.UI.Interfaces
{
    public interface IResultCardBuilder
    {
        MaterialCard CreateResultCard((int songId, string title, string artist, int matches, double confidence) result);
        
        MaterialCard CreateNoResultsCard();
    }
}