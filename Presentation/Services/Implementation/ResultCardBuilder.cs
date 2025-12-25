using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using System.Windows.Forms;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class ResultCardBuilder : IResultCardBuilder
    {
        public MaterialCard CreateResultCard(SearchResultModel searchResultModel)
        {
            var card = new MaterialCard()
            {
                Size = new Size(520, 120),
                Margin = new Padding(0, 0, 0, 15)
            };

            var lblTitle = new MaterialLabel()
            {
                Text = TruncateText(searchResultModel.Song.Title, 65),
                Tag = searchResultModel.Song.Title,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblArtist = new MaterialLabel()
            {
                Text = TruncateText(searchResultModel.Song.Artist, 30),
                Tag = searchResultModel.Song.Artist,
                Location = new Point(20, 60),
                AutoSize = true
            };

            var toolTip = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true,
            };

            toolTip.SetToolTip(lblTitle, searchResultModel.Song.Title);
            toolTip.SetToolTip(lblArtist, searchResultModel.Song.Artist);

            var lblMatches = new MaterialLabel()
            {
                Text = $"Совпадений: {searchResultModel.Matches}",
                Location = new Point(360, 60),
                AutoSize = true
            };

            var divider = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            card.Controls.AddRange(new Control[] { lblTitle, lblArtist, lblMatches, divider });
            return card;
        }

        public MaterialCard CreateNoResultsCard()
        {
            var card = new MaterialCard()
            {
                Size = new Size(500, 120),
                Margin = new Padding(0, 0, 0, 15)
            };

            var noResultsLabel = new MaterialLabel()
            {
                Text = $"Ничего не распознано {Environment.NewLine}Попробуйте еще раз",
                Location = new Point(20, 20),
                Size = new Size(460, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var divider = new MaterialDivider()
            {
                Height = 2,
                Dock = DockStyle.Bottom
            };

            card.Controls.AddRange(new Control[] { noResultsLabel, divider });
            return card;
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}