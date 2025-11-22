using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Controls
{
    public partial class SettingsStateControl : UserControl
    {
        private readonly MainForm _mainForm;

        public SettingsStateControl(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
        }

        private void FABtnReadyStateControl_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Ready);
        }
    }
}
