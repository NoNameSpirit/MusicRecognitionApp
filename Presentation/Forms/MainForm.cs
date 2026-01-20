using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Core.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Forms
{
    public partial class MainForm : BaseForm, IApplicationForm
    {
        private readonly IMessageBox _messageBoxService;
        private readonly IStateManagerService _stateManagerService;
        
        public MainForm(
            IMessageBox messageBoxService,
            IStateManagerService stateManagerService)
        {
            InitializeComponent();

            _messageBoxService = messageBoxService;
            _stateManagerService = stateManagerService;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = _messageBoxService
                .ShowWarning("Do you really want to close the app?");

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        
        private async void MainForm_Load(object sender, EventArgs e)
        {
            _stateManagerService.Initialize(this);
            await _stateManagerService.SetStateAsync(AppState.Ready);
        }

        public void AddControl(UserControl control)
            => Controls.Add(control);

        public void RemoveControl(UserControl control)
            => Controls.Remove(control);

        public void BringToFront(UserControl control)
            => control.BringToFront();

        public void SetVisibility(UserControl control, bool visible)
            => control.Visible = visible;
    }
}
