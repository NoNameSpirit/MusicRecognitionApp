using MaterialSkin.Controls;
using MusicRecognitionApp.Services.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicRecognitionApp.Services
{
    public class CardService : ICardService
    {
        private MaterialButton _btnSongs;
        private MaterialButton _btnAuthors;
        private FlowLayoutPanel _panelOfCards;

        private readonly IAudioDatabase _databaseService;

        public CardService(IAudioDatabase databaseService) 
        {
            _databaseService = databaseService;
        }

        public void Initialize(MaterialButton BtnSongs, MaterialButton BtnAuthors, FlowLayoutPanel FLPanelOfCards)
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

        private void AddSongCards()
        {
            var recognizedSongs = _databaseService.GetRecognizedSongs();

            if (!recognizedSongs.Any())
            {
                ShowNoSongsMessage();
                return;
            }

            foreach (var song in recognizedSongs)
            {
                var songCard = CreateSongCard(song);
                _panelOfCards.Controls.Add(songCard);
            }
        }

        private void AddAuthorCards()
        {
            var recognizedArtists = _databaseService.GetRecognizedArtists();

            if (!recognizedArtists.Any())
            {
                ShowNoArtistsMessage();
                return;
            }

            foreach (var artist in recognizedArtists)
            {
                var authorCard = CreateAuthorCard(artist);
                _panelOfCards.Controls.Add(authorCard);
            }
        }

        private MaterialCard CreateSongCard((int songId, string title, string artist, int matches, DateTime recognitionDate) song)
        {
            var songCard = new MaterialCard()
            {
                Size = new Size(520, 80),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblTitle = new MaterialLabel()
            {
                Text = song.title,
                Font = new Font("Roboto", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblArtist = new MaterialLabel()
            {
                Text = song.artist,
                Location = new Point(15, 40),
                AutoSize = true
            };

            var lblMatches = new MaterialLabel()
            {
                Text = $"Совпадений: {song.matches}",
                Location = new Point(350, 30),
                AutoSize = true
            };

            var divider = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            songCard.Controls.AddRange(new Control[] { lblTitle, lblArtist, lblMatches, divider });

            return songCard;
        }

        private MaterialCard CreateAuthorCard((string artist, int songCount) artistInfo)
        {
            var authorCard = new MaterialCard()
            {
                Size = new Size(520, 100),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblName = new MaterialLabel()
            {
                Text = artistInfo.artist,
                Font = new Font("Roboto", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var lblSongs = new MaterialLabel()
            {
                Text = $"{artistInfo.songCount} {(artistInfo.songCount == 1 ? "трек распознан" : "треков распознано")}",
                Location = new Point(15, 40),
                AutoSize = true
            };

            var divider = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            authorCard.Controls.AddRange(new Control[] { lblName, lblSongs, divider });

            return authorCard;
        }

        private void ShowNoSongsMessage()
        {
            var messageCard = new MaterialCard()
            {
                Size = new Size(520, 60),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblMessage = new MaterialLabel()
            {
                Text = "Нет распознанных треков",
                Font = new Font("Roboto", 12, FontStyle.Regular),
                Location = new Point(15, 20),
                AutoSize = true
            };

            messageCard.Controls.Add(lblMessage);
            _panelOfCards.Controls.Add(messageCard);
        }

        private void ShowNoArtistsMessage()
        {
            var messageCard = new MaterialCard()
            {
                Size = new Size(520, 60),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblMessage = new MaterialLabel()
            {
                Text = "Нет распознанных исполнителей",
                Font = new Font("Roboto", 12, FontStyle.Regular),
                Location = new Point(15, 20),
                AutoSize = true
            };

            messageCard.Controls.Add(lblMessage);
            _panelOfCards.Controls.Add(messageCard);
        }
    }
}
