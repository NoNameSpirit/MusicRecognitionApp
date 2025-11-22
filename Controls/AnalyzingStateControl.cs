using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        
        public AnalyzingStateControl(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
        }

        private void BtnStopRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Ready);
        }
    }
}
