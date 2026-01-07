namespace MusicRecognitionApp.Presentation.Controls
{
    public partial class SongCard : UserControl
    {
        public event EventHandler CopyRequest;

        public SongCard(string title, string artist, int matches)
        {
            InitializeComponent();

            this.lblTitle.Text = TruncateText(title, 40);
            this.lblArtist.Text = TruncateText(artist, 30);
            this.lblMatches.Text = $"Совпадений: {matches}";

            toolTip.SetToolTip(lblTitle, title);
            toolTip.SetToolTip(lblArtist, artist);
            notificationToolTip.SetToolTip(btnCopy, "Копировать");

            btnCopy.Tag = $"{title} - {artist}";
            btnCopy.Click += OnCopyButtonClick;
        }

        private void OnCopyButtonClick(object sender, EventArgs e)
        {
            string? text = btnCopy.Tag?.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }

            notificationToolTip.Show("Скопировано", btnCopy);

            CopyRequest?.Invoke(this, EventArgs.Empty);
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}
