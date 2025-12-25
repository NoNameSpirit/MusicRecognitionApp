using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IStateRegistry
    {
        public UserControl CreateStateControl(MainForm mainForm, AppState state);

        public IEnumerable<AppState> GetStatesControls();
    }
}
