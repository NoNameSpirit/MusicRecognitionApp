using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.History;
using MusicRecognitionApp.Services.Interfaces;
using System.Windows.Forms;

namespace MusicRecognitionApp.Services
{
    public class CardService : ICardService
    {
        private MaterialButton _btnSongs;
        private MaterialButton _btnAuthors;
        private FlowLayoutPanel _panelOfCards;

        private readonly IRecognitionSongService _recognitionSongService;

        public CardService(IRecognitionSongService recognitionSongService) 
        {
            _recognitionSongService = recognitionSongService;
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
            var recognizedSongs = _recognitionSongService.GetRecognizedSongs();

            if (!recognizedSongs.Any())
            {
                ShowNoSongsMessage();
                return;
            }

            foreach (var recognizedSong in recognizedSongs)
            {
                var songCard = CreateSongCard(recognizedSong);
                _panelOfCards.Controls.Add(songCard);
            }
        }

        private void AddAuthorCards()
        {
            var recognizedArtists = _recognitionSongService.GetRecognizedArtists();

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

        private MaterialCard CreateSongCard(RecognizedSongModel recognizedSong)
        {
            var songCard = new MaterialCard()
            {
                Size = new Size(520, 100),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblTitle = new MaterialLabel()
            {
                Text = TruncateText(recognizedSong.Song.Title, 65),
                Tag = recognizedSong.Song.Title,
                Location = new Point(15, 15),
                AutoSize = true,
            };

            var lblArtist = new MaterialLabel()
            {
                Text = TruncateText(recognizedSong.Song.Artist, 30),
                Tag = recognizedSong.Song.Artist,
                Location = new Point(15, 60),
                AutoSize = true
            };

            var toolTip = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true,
            };

            toolTip.SetToolTip(lblTitle, recognizedSong.Song.Title);
            toolTip.SetToolTip(lblArtist, recognizedSong.Song.Artist);

            var lblMatches = new MaterialLabel()
            {
                Text = $"Совпадений: {recognizedSong.Matches}",
                Location = new Point(360, 60),
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

        private MaterialCard CreateAuthorCard(ArtistStatisticModel artistStatistic)
        {
            var authorCard = new MaterialCard()
            {
                Size = new Size(520, 100),
                Margin = new Padding(0, 0, 0, 10)
            };

            string songsText = $"{artistStatistic.SongCount} {(artistStatistic.SongCount == 1 ? "трек распознан" : "треков распознано")}";
            var lblSongs = new MaterialLabel()
            {
                Text = TruncateText(songsText, 50),
                Tag = songsText,
                Location = new Point(15, 60),
                AutoSize = true
            };

            var lblArtist = new MaterialLabel()
            {
                Text = TruncateText(artistStatistic.Artist, 30),
                Tag = artistStatistic.Artist,
                Location = new Point(15, 15),
                AutoSize = true
            };

            var toolTip = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true,
            };

            toolTip.SetToolTip(lblSongs, songsText);
            toolTip.SetToolTip(lblArtist, artistStatistic.Artist);

            var divider = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            authorCard.Controls.AddRange(new Control[] { lblArtist, lblSongs, divider });

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
                Location = new Point(15, 20),
                AutoSize = true
            };

            messageCard.Controls.Add(lblMessage);
            _panelOfCards.Controls.Add(messageCard);
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength-3) + "...";
        }
    }
}
