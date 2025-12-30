using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IStateRegistry
    {
        UserControl CreateStateControl(MainForm mainForm, AppState state);

        IEnumerable<AppState> GetStatesControls();
    }
}
