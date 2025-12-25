using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IStateRegistry
    {
        public UserControl CreateStateControl(MainForm mainForm, AppState state);

        public IEnumerable<AppState> GetStatesControls();
    }
}
