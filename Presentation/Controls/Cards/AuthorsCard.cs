namespace MusicRecognitionApp.Presentation.Controls
{
    public partial class AuthorsCard : UserControl
    {
        public event EventHandler CopyRequest;

        public AuthorsCard(string artist, int trackCount)
        {
            InitializeComponent();

            string title = $"Треков распознано: {trackCount}";
            
            this.lblTitle.Text = TruncateText(title, 50);
            this.lblArtist.Text = TruncateText(artist, 30);

            toolTip.SetToolTip(lblTitle, title);
            toolTip.SetToolTip(lblArtist, artist);
            notificationToolTip.SetToolTip(btnCopy, "Копировать");

            btnCopy.Tag = $"{artist}";
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
