using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IStateManagerService
    {
        void Initialize(MainForm mainForm);
        
        Task SetStateAsync(AppState newState);
        
        Task SetStateAsync(AppState newState, object? stateData);

    }
}
