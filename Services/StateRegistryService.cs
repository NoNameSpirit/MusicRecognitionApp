using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Services
{
    public static class StateRegistryService
    {
        private static readonly Dictionary<AppState, Func<MainForm, UserControl>> _factories = new()
        {
            {AppState.Ready, mainForm => new ReadyStateControl(mainForm) },
        };

        public static UserControl CreateStateControl(MainForm mainForm, AppState state)
        {
            if (_factories.TryGetValue(state, out var factorie))
                return factorie(mainForm);

            throw new Exception($"Don't have this factrie for {state}");
        }

        public static IEnumerable<AppState> GetStatesControls()
            => _factories.Keys;
    }
}
