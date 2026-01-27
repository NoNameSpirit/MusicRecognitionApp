using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class StateManagerService : IStateManagerService
    {
        private AppState _currentState;
        private Dictionary<AppState, UserControl> _states = new();
        private MainForm _form; 

        private readonly IStateRegistry _stateRegistryService;
        
        public StateManagerService(
            IStateRegistry stateRegistryService,
            IServiceProvider serviceProvider)
        {
            _stateRegistryService = stateRegistryService;
        }

        public async Task SetStateAsync(AppState newState)
        {
            await SetStateInternalAsync(newState, null);
        }

        public async Task SetStateAsync(AppState newState, object? stateData)
        {
            await SetStateInternalAsync(newState, stateData);
        }

        private async Task SetStateInternalAsync(AppState newState, object? stateData)
        {
            
            if (_states.ContainsKey(_currentState))
                _form.SetVisibility(_states[_currentState], false);

            _currentState = newState;
            InitializeState();

            if (_states[_currentState] is IStateWithData stateWithData)
            {
                stateWithData.SetStateData(stateData);
            }

            _form.SetVisibility(_states[_currentState], true);
            _form.BringToFront(_states[_currentState]);
        }

        private void InitializeState()
        {
            if (_states.ContainsKey(_currentState))
                return;

            var control = _stateRegistryService.CreateStateControl(_currentState, this);

            control.Dock = DockStyle.Fill;
            control.Visible = false;

            _states[_currentState] = control;
            _form.AddControl(control);
        }

        public void Initialize(MainForm form)
        {
            _form = form;
        }
    }
}
