using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Controls
{
    public partial class RecordingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        
        public RecordingStateControl(MainForm mainForm)
        {
            InitializeComponent();
    
            _mainForm = mainForm;
        }

        private void BtnStopRecognition_Click(object sender, EventArgs e)
        {

        }
    }
}
