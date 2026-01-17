using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICardService _cardService;
        private readonly IMessageBox _messageBoxService;
        private readonly IAnimationService _animationService;
        private readonly IRecognitionSongService _recognitionSongService;
        private readonly ISongAddingService _songAddingService;

        public StateRegistryService(
            IServiceProvider serviceProvider,
            IMessageBox messageBoxService,
            ICardService cardService,
            IAnimationService animationService,
            IRecognitionSongService recognitionSongService,
            ISongAddingService songAddingService)
        {
            _serviceProvider = serviceProvider;
            _messageBoxService = messageBoxService;
            _cardService = cardService;
            _animationService = animationService;
            _recognitionSongService = recognitionSongService;
            _songAddingService = songAddingService;
        }

        public UserControl CreateStateControl(AppState state)
        {
            var _stateManagerService = _serviceProvider.GetRequiredService<IStateManagerService>();

            return state switch
            {
                AppState.Ready => new ReadyStateControl(_stateManagerService, _animationService, _messageBoxService, _songAddingService),

                AppState.Recording => new RecordingStateControl(_stateManagerService, _serviceProvider),

                AppState.Analyzing => new AnalyzingStateControl(_stateManagerService, _serviceProvider),

                AppState.Result => new ResultStateControl(_stateManagerService, _messageBoxService, _cardService, _recognitionSongService),

                AppState.Library => new LibraryStateControl(_stateManagerService, _cardService),

                AppState.Processing => new ProcessingStateControl(_stateManagerService),

                _ => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Processing };
    }
}
