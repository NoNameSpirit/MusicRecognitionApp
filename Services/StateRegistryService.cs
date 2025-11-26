using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Interfaces;
using System;

namespace MusicRecognitionApp.Services
{
    public class StateRegistryService : IStateRegistry
    {
        private readonly ICardService cardService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAudioRecognition _audioRecognitionService;
        private readonly IAudioRecorder _audioRecorderService;
        
            
        public StateRegistryService(
            IServiceProvider serviceProvider,
            IAudioRecognition audioRecognitionService,
            IAudioRecorder audioRecorderService)
        {
            _serviceProvider = serviceProvider;
            _audioRecognitionService = audioRecognitionService;
            _audioRecorderService = audioRecorderService;
        }

        public UserControl CreateStateControl(MainForm mainForm, AppState state)
        {
            return state switch
            {
                AppState.Ready => new ReadyStateControl(mainForm, _audioRecognitionService),
                AppState.Recording => new RecordingStateControl(mainForm),
                AppState.Analyzing => new AnalyzingStateControl(mainForm, _audioRecorderService),
                AppState.Result => new ResultStateControl(mainForm),
                AppState.Library => new LibraryStateControl(mainForm, _serviceProvider),
                AppState.Settings => new SettingsStateControl(mainForm),
                _ => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Settings };
    }
}
