using MaterialSkin.Controls;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface ICardService
    {
        public void ShowSongs();

        public void ShowAuthors();

        public void Initialize(MaterialButton BtnSongs, MaterialButton BtnAuthors, FlowLayoutPanel FLPanelOfCards);
    }
}
