namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IMessageBoxService
    {
        DialogResult ShowWarning(string message);

        DialogResult ShowQuestion(string message);

        void ShowError(string message);

        void ShowInfo(string message);
    }
}
