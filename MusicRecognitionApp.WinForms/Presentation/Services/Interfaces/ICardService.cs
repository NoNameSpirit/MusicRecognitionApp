using MaterialSkin.Controls;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Presentation.Controls;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface ICardService
    {
        void ShowSongs();

        void ShowAuthors();

        void Initialize(MaterialButton btnSongs, MaterialButton btnAuthors, FlowLayoutPanel panelOfCards);

        NoInfoCard ShowNoSongsCard();

        SongCard CreateResultCard(SearchResult searchResultModel);
        
        NoInfoCard CreateNoResultsCard();
    }
}
