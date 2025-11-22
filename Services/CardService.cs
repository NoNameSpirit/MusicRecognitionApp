using MaterialSkin.Controls;
using MusicRecognitionApp.Services.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicRecognitionApp.Services
{
    public class CardService : ICardService
    {
        private readonly MaterialButton _btnSongs;
        private readonly MaterialButton _btnAuthors;
        private readonly FlowLayoutPanel _panelOfCards;

        public CardService(MaterialButton BtnSongs, MaterialButton BtnAuthors, FlowLayoutPanel FLPanelOfCards) 
        {
            _btnSongs = BtnSongs;
            _btnAuthors = BtnAuthors;
            _panelOfCards = FLPanelOfCards;
        }

        public void ShowSongs()
        {   
            SetBtnsStyles(true);
            _panelOfCards.Controls.Clear();
            AddSongCards();
        }

        public void ShowAuthors() 
        {
            SetBtnsStyles(false);
            _panelOfCards.Controls.Clear();
            AddAuthorCards();
        }

        private void SetBtnsStyles(bool isSongsActive)
        {
            _btnSongs.Type = isSongsActive
                ? MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
                : MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;

            _btnAuthors.Type = isSongsActive
                ? MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined
                : MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
        }

        //need to do from DB (SQLite)
        private void AddSongCards()
        {
            var songCard1 = new MaterialCard()
            {
                Size = new Size(520, 80),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblTitle1 = new MaterialLabel()
            {
                Text = "Bad Guy",
                Font = new Font("Roboto", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblArtist1 = new MaterialLabel()
            {
                Text = "Billie Eilish",
                Location = new Point(15, 40),
                AutoSize = true
            };

            var lblDuration1 = new MaterialLabel()
            {
                Text = "3:14",
                Location = new Point(450, 30),
                AutoSize = true
            };

            var divider1 = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            songCard1.Controls.AddRange(new Control[] { lblTitle1, lblArtist1, lblDuration1, divider1 });

            _panelOfCards.Controls.Add(songCard1);
        }

        //same from DB (SQLite)
        private void AddAuthorCards()
        {
            var authorCard1 = new MaterialCard()
            {
                Size = new Size(520, 100),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblName1 = new MaterialLabel()
            {
                Text = "Billie Eilish",
                Font = new Font("Roboto", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblSongs1 = new MaterialLabel()
            {
                Text = "45 songs recognized",
                Location = new Point(15, 40),
                AutoSize = true
            };

            var lblPopular1 = new MaterialLabel()
            {
                Text = "Popular: Bad Guy",
                Location = new Point(15, 65),
                AutoSize = true
            };

            var divider1 = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            authorCard1.Controls.AddRange(new Control[] { lblName1, lblSongs1, lblPopular1, divider1 });

            _panelOfCards.Controls.Add(authorCard1);
        }
    }
}
