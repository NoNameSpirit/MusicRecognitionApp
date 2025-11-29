using MaterialSkin.Controls;
using MusicRecognitionApp.Services.UI.Interfaces;

namespace MusicRecognitionApp.Services.UI
{
    public class ResultCardBuilder : IResultCardBuilder
    {
        public MaterialCard CreateResultCard((int songId, string title, string artist, int matches, double confidence) result)
        {
            var card = new MaterialCard()
            {
                Size = new Size(500, 120),
                Margin = new Padding(0, 0, 0, 15)
            };

            var lblTitle = new MaterialLabel()
            {
                Text = result.title,
                Font = new Font("Roboto", 16F, FontStyle.Bold, GraphicsUnit.Pixel),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblArtist = new MaterialLabel()
            {
                Text = result.artist,
                Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel),
                Location = new Point(20, 45),
                AutoSize = true
            };

            var lblMatches = new MaterialLabel()
            {
                Text = $"Совпадений: {result.matches}",
                Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel),
                Location = new Point(200, 70),
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
                Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel),
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
    }
}