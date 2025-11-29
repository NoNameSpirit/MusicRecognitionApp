using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Services
{
    public class MessageBoxService : IMessageBox
    {
        public DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
