using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;

namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IStateRegistry
    {
        public UserControl CreateStateControl(MainForm mainForm, AppState state);

        public IEnumerable<AppState> GetStatesControls();
    }
}
