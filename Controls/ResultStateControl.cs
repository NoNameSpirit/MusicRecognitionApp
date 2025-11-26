using MaterialSkin.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Controls
{
    public partial class ResultStateControl : UserControl
    {
        private readonly MainForm _mainForm;

        public ResultStateControl(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
            DisplayResults();
        }

        private void DisplayResults()
        {
            var results = _mainForm.GetRecognitionResults();

            if (results == null || results.Count == 0)
            {
                ShowNoResults();
                return;
            }

            foreach (var result in results.Take(1)) 
            {
                var resultCard = CreateResultCard(result);
                PanelResults.Controls.Add(resultCard);
            }
        }

        private MaterialCard CreateResultCard((int songId, string title, string artist, int matches, double confidence) result)
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

            var lblConfidence = new MaterialLabel()
            {
                Text = $"Точность: {result.confidence:P1}",
                Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel),
                Location = new Point(20, 70),
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

            card.Controls.AddRange(new Control[] { lblTitle, lblArtist, lblConfidence, lblMatches, divider });

            return card;
        }

        private void ShowNoResults()
        {
            var noResultsLabel = new MaterialLabel()
            {
                Text = $"Ничего не распознано {Environment.NewLine}Попробуйте еще раз",
                Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel),
                Location = new Point(150, 200),
                Size = new Size(300, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };

            PanelResults.Controls.Add(noResultsLabel);
        }

        private void FABtnSettings_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Settings);
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }

        private void BtnBackToReady_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Ready);
        }
    }
}
