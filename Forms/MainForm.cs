using MaterialSkin.Controls;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Forms
{
    public partial class MainForm : BaseForm
    {
        private AppState _currentState;
        private Dictionary<AppState, UserControl> _states = new();
        private List<(int songId, string title, string artist, int matches, double confidence)> _lastResults;

        private readonly IMessageBox _messageBoxService;
        private readonly IStateRegistry _stateRegistryService;
        public MainForm(IMessageBox messageBoxService, IStateRegistry stateRegistryService)
        {
            InitializeComponent();

            _messageBoxService = messageBoxService;
            _stateRegistryService = stateRegistryService;

            SetState(AppState.Ready);
        }

        private void InitializeState()
        {
            if (!_states.ContainsKey(_currentState))
            {
                var control = _stateRegistryService.CreateStateControl(this, _currentState);

                control.Dock = DockStyle.Fill;
                control.Visible = false;

                _states[_currentState] = control;
                this.Controls.Add(control);
            }
        }

        public void SetState(AppState newState)
        {
            if (_states.ContainsKey(_currentState))
                _states[_currentState].Visible = false;

            _currentState = newState;
            InitializeState();

            _states[_currentState].Visible = true;
            _states[_currentState].BringToFront();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = _messageBoxService
                .ShowWarning("Вы действительно хотите закрыть приложение?");

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public void SetRecognitionResults(List<(int songId, string title, string artist, int matches, double confidence)> results)
        {
            _lastResults = results;
        }

        public List<(int songId, string title, string artist, int matches, double confidence)> GetRecognitionResults()
        {
            return _lastResults;
        }
    }
}
