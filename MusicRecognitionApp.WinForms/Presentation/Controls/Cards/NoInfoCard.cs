namespace MusicRecognitionApp.Presentation.Controls
{
    public partial class NoInfoCard : UserControl
    {
        public NoInfoCard(string title)
        {
            InitializeComponent();

            this.lblTitle.Text = TruncateText(title, 50);

            toolTip.SetToolTip(lblTitle, title);
        }

        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}
