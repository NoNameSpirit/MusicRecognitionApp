using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly ICardService cardService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAudioRecognition _recognitionService;
        private readonly IAudioRecorder _recorderService;
        private readonly IMessageBox _messageBoxService;
        private readonly IAnimationService _animationService;
        private readonly IResultCardBuilder _resultCardBuilder;
        private readonly IRecognitionSongService _recognitionSongService;


        public StateRegistryService(
            IServiceProvider serviceProvider,
            IAudioRecognition audioRecognitionService,
            IAudioRecorder audioRecorderService,
            IMessageBox messageBoxService,
            IAnimationService animationService,
            IResultCardBuilder resultCardBuilder,
            IRecognitionSongService recognitionSongService)
        {
            _serviceProvider = serviceProvider;
            _recognitionService = audioRecognitionService;
            _recorderService = audioRecorderService;
            _messageBoxService = messageBoxService;
            _animationService = animationService;
            _resultCardBuilder = resultCardBuilder;
            _recognitionSongService = recognitionSongService;
        }

        public UserControl CreateStateControl(MainForm mainForm, AppState state)
        {
            return state switch
            {
                AppState.Ready => new ReadyStateControl(mainForm, _recognitionService, _animationService, _messageBoxService),

                AppState.Recording => new RecordingStateControl(mainForm, _recorderService),

                AppState.Analyzing => new AnalyzingStateControl(mainForm, _recognitionService),

                AppState.Result => new ResultStateControl(mainForm, _messageBoxService, _resultCardBuilder, _recognitionSongService),

                AppState.Library => new LibraryStateControl(mainForm, _serviceProvider),

                AppState.Processing => new ProcessingStateControl(mainForm, _recognitionService),

                _ => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Processing };
    }
}
