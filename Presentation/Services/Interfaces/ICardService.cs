using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Controls;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface ICardService
    {
        void ShowSongs();

        void ShowAuthors();

        void Initialize(MaterialButton btnSongs, MaterialButton btnAuthors, FlowLayoutPanel panelOfCards);

        NoInfoCard ShowNoSongsCard();

        SongCard CreateResultCard(SearchResultModel searchResultModel);
        
        NoInfoCard CreateNoResultsCard();
    }
}
