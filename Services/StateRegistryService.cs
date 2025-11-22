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

        public StateRegistryService()
        {
        }

        public UserControl CreateStateControl(MainForm mainForm, AppState state)
        {
            return state switch
            {
                AppState.Ready     => new ReadyStateControl(mainForm),
                AppState.Recording => new RecordingStateControl(mainForm),
                AppState.Analyzing => new AnalyzingStateControl(mainForm),
                AppState.Result    => new ResultStateControl(mainForm),
                AppState.Library   => new LibraryStateControl(mainForm),
                AppState.Settings  => new SettingsStateControl(mainForm),
                _                  => throw new Exception($"Don't have this factory for {state}")
            };
        }

        public IEnumerable<AppState> GetStatesControls()
            => new[] { AppState.Ready, AppState.Recording, AppState.Analyzing,
                   AppState.Result, AppState.Library, AppState.Settings };
    }
}
