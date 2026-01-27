using MaterialSkin.Controls;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Presentation.Controls;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
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

        public void Initialize(MaterialButton btnSongs, MaterialButton btnAuthors, FlowLayoutPanel panelOfCards)
        {
            _btnSongs = btnSongs;
            _btnAuthors = btnAuthors;
            _panelOfCards = panelOfCards;
        }

        public void ShowSongs()
        {
            SetBtnsStyles(true);
            
            _panelOfCards.Controls.Clear();
            
            var songs = _recognitionSongService.GetRecognizedSongs();

            if (!songs.Any())
            {
                var card = CreateNoInfoCard("There are no recognized tracks");

                _panelOfCards.Controls.Add(card);

                return;
            }

            foreach (var song in songs)
            {
                var songCard = CreateSongCard(song.Song.Title, song.Song.Artist, song.Matches);

                _panelOfCards.Controls.Add(songCard);
            };
        }

        public void ShowAuthors()
        {
            SetBtnsStyles(false);
            
            var recognizedArtists = _recognitionSongService.GetRecognizedArtists();

            if (!recognizedArtists.Any())
            {
                var card = CreateNoInfoCard("There are no recognized artists");

                _panelOfCards.Controls.Add(card);

                return;
            }

            foreach (var artistStatistic in recognizedArtists)
            {
                var authorCard = CreateAuthorCard(
                    artistStatistic.Artist,
                    artistStatistic.SongCount);

                _panelOfCards.Controls.Add(authorCard);
            }
        }

        public NoInfoCard ShowNoSongsCard()
            => CreateNoInfoCard("There are no recognized tracks");
        
        public NoInfoCard CreateNoResultsCard()
            => CreateNoInfoCard($"Nothing is recognized {Environment.NewLine}Try again");

        public SongCard CreateResultCard(SearchResult searchResultModel)
            => CreateSongCard(searchResultModel.Song.Title, searchResultModel.Song.Artist, searchResultModel.Matches);
        
        private SongCard CreateSongCard(string title, string artist, int matches)
            => new SongCard(title, artist, matches);

        private AuthorsCard CreateAuthorCard(string artist, int trackCount)
            => new AuthorsCard(artist, trackCount);

        private NoInfoCard CreateNoInfoCard(string title)
            => new NoInfoCard(title);

        private void SetBtnsStyles(bool isSongsActive)
        {
            _btnSongs.Type = isSongsActive
                ? MaterialButton.MaterialButtonType.Contained
                : MaterialButton.MaterialButtonType.Outlined;

            _btnAuthors.Type = isSongsActive
                ? MaterialButton.MaterialButtonType.Outlined
                : MaterialButton.MaterialButtonType.Contained;
        }
    }
}
