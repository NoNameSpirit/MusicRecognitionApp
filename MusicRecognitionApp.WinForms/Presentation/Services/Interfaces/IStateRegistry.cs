using MusicRecognitionApp.Core.Enums;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IStateRegistry
    {
        UserControl CreateStateControl(AppState state, IStateManagerService stateManagerService);

        IEnumerable<AppState> GetStatesControls();
    }
}
