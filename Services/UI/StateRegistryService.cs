using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI;
using MusicRecognitionApp.Services.UI.Interfaces;
using System;

namespace MusicRecognitionApp.Services
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly ICardService cardService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAudioRecognition _recognitionService;
        private readonly IAudioRecorder _recorderService;
        private readonly IAudioDatabase _databaseService;
        private readonly IMessageBox _messageBoxService;
        private readonly IAnimationService _animationService;
        private readonly IResultCardBuilder _resultCardBuilder;

        public StateRegistryService(
            IServiceProvider serviceProvider,
            IAudioRecognition audioRecognitionService,
            IAudioRecorder audioRecorderService,
            IAudioDatabase databaseService,
            IMessageBox messageBoxService,
            IAnimationService animationService,
            IResultCardBuilder resultCardBuilder)
        {
            _serviceProvider = serviceProvider;
            _recognitionService = audioRecognitionService;
            _recorderService = audioRecorderService;
            _databaseService = databaseService;
            _messageBoxService = messageBoxService;
            _animationService = animationService;
            _resultCardBuilder = resultCardBuilder;
        }

        public UserControl CreateStateControl(MainForm mainForm, AppState state)
        {
            return state switch
            {
                AppState.Ready => new ReadyStateControl(mainForm, _recognitionService, _animationService, _messageBoxService),
                
                AppState.Recording => new RecordingStateControl(mainForm, _recorderService),
                
                AppState.Analyzing => new AnalyzingStateControl(mainForm, _recognitionService),
                
                AppState.Result => new ResultStateControl(mainForm, _databaseService, _messageBoxService, _resultCardBuilder),
                
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
