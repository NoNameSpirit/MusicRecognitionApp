namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IMessageBox
    {
        DialogResult ShowWarning(string message);

        DialogResult ShowQuestion(string message);

        void ShowError(string message);

        void ShowInfo(string message);
    }
}
