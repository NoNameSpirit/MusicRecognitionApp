using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Implementation;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ResultStateControl : UserControl, IStateWithData
    {
        private List<SearchResult> _results;
        
        private readonly IStateManagerService _stateManagerService;
        private readonly IResultDisplayService _resultDisplayService;
        private readonly IMessageBoxService _messageBoxService;

        public ResultStateControl(
            IStateManagerService stateManagerService,
            IResultDisplayService resultDisplayService,
            IMessageBoxService messageBoxService)
        {   
            _stateManagerService = stateManagerService;
            _resultDisplayService = resultDisplayService;
            _messageBoxService = messageBoxService;

            InitializeComponent();
        }

        protected override async void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                try
                {
                    await _resultDisplayService.DisplayResults(PanelResults, PicRecordingGif, _results);
                }
                catch (Exception ex)
                {
                    _messageBoxService.ShowError($"Error saving a recognized track: {ex.Message}");
                }
            }
            else
            {
                _resultDisplayService.ClearResults(PanelResults);
            }
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _stateManagerService.SetStateAsync(AppState.Library);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _stateManagerService.SetStateAsync(AppState.Library);
        }

        private void BtnBackToReady_Click(object sender, EventArgs e)
        {
            _stateManagerService.SetStateAsync(AppState.Ready);
        }

        public void SetStateData(object? stateData)
        {
            _results = stateData as List<SearchResult>;
            _resultDisplayService.ClearResults(PanelResults);
        }
    }
}
