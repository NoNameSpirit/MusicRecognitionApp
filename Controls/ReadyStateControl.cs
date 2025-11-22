using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly MainForm _mainForm;

        public ReadyStateControl(MainForm mainForm)
        {
            InitializeComponent();
            
            _mainForm = mainForm;
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }

        private void FABtnSettings_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Settings);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }
    }
}
