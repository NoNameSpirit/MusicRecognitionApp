using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly ICardService _cardService;
        private readonly IMessageBox _messageBoxService;
        private readonly IAnimationService _animationService;
        private readonly IRecognitionSongService _recognitionSongService;
        private readonly ISongAddingService _songAddingService;
        private readonly IRecordingSessionService _recordingSessionService;
        private readonly IAnalyzingSessionService _analyzingSessionService;

        public StateRegistryService(
            IMessageBox messageBoxService,
            ICardService cardService,
            IAnimationService animationService,
            IRecognitionSongService recognitionSongService,
            ISongAddingService songAddingService,
            IRecordingSessionService recordingSessionService,
            IAnalyzingSessionService analyzingSessionService)
        {
            _messageBoxService = messageBoxService;
            _cardService = cardService;
            _animationService = animationService;
            _recognitionSongService = recognitionSongService;
            _songAddingService = songAddingService;
            _recordingSessionService = recordingSessionService;
            _analyzingSessionService = analyzingSessionService;
        }

        public UserControl CreateStateControl(AppState state, IStateManagerService stateManagerService)
        {
            return state switch
            {
                AppState.Ready => new ReadyStateControl(stateManagerService, _animationService, _messageBoxService, _songAddingService),

                AppState.Recording => new RecordingStateControl(stateManagerService, _recordingSessionService),

                AppState.Analyzing => new AnalyzingStateControl(stateManagerService, _analyzingSessionService),

                AppState.Result => new ResultStateControl(stateManagerService, _messageBoxService, _cardService, _recognitionSongService),

                AppState.Library => new LibraryStateControl(stateManagerService, _cardService),

                AppState.Processing => new ProcessingStateControl(stateManagerService),

                _ => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Processing };
    }
}
