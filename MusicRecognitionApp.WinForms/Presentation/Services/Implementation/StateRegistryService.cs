using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly ICardService _cardService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IAnimationService _animationService;
        private readonly ISongAddingService _songAddingService;
        private readonly IRecordingSessionService _recordingSessionService;
        private readonly IAnalyzingSessionService _analyzingSessionService;
        private readonly IProcessingAudio _processingAudio;
        private readonly IResultDisplayService _resultDisplayService;

        public StateRegistryService(
            IMessageBoxService messageBoxService,
            ICardService cardService,
            IAnimationService animationService,
            ISongAddingService songAddingService,
            IRecordingSessionService recordingSessionService,
            IAnalyzingSessionService analyzingSessionService,
            IProcessingAudio processingAudio,
            IResultDisplayService resultDisplayService)
        {
            _messageBoxService = messageBoxService;
            _cardService = cardService;
            _animationService = animationService;
            _songAddingService = songAddingService;
            _recordingSessionService = recordingSessionService;
            _analyzingSessionService = analyzingSessionService;
            _processingAudio = processingAudio;
            _resultDisplayService = resultDisplayService;
        }

        public UserControl CreateStateControl(AppState state, IStateManagerService stateManagerService)
        {
            return state switch
            {
                AppState.Ready => new ReadyStateControl(stateManagerService, _animationService, _messageBoxService, _songAddingService),

                AppState.Recording => new RecordingStateControl(stateManagerService, _recordingSessionService, _messageBoxService),

                AppState.Analyzing => new AnalyzingStateControl(stateManagerService, _analyzingSessionService, _messageBoxService),

                AppState.Result => new ResultStateControl(stateManagerService, _resultDisplayService, _messageBoxService),

                AppState.Library => new LibraryStateControl(stateManagerService, _cardService),

                AppState.Processing => new ProcessingStateControl(stateManagerService, _processingAudio),

                _ => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Processing };
    }
}
