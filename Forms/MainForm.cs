using MaterialSkin.Controls;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services;

namespace MusicRecognitionApp.Forms
{
    public partial class MainForm : BaseForm
    {
        private AppState _currentState;
        private Dictionary<AppState, UserControl> _states;

        public MainForm()
        {
            InitializeComponent();
            
            InitializeStates();
            
            SetState(AppState.Ready);
        }

        private void InitializeStates()
        {
            _states = new Dictionary<AppState, UserControl>();

            foreach (var state in StateRegistryService.GetStatesControls())
            {
                var control = StateRegistryService.CreateStateControl(this, state);
                
                control.Dock = DockStyle.Fill;
                control.Visible = false;

                _states[state] = control;
                this.Controls.Add(control);
            }
        }

        private void SetState(AppState newState)
        {
            if (_states.ContainsKey(_currentState))
                _states[_currentState].Visible = false;

            _currentState = newState;
            _states[_currentState].Visible = true;
            _states[_currentState].BringToFront();
        }
    }
}
