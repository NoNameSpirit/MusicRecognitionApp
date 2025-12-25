namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface IMessageBox
    {
        public DialogResult ShowWarning(string message);

        public DialogResult ShowQuestion(string message);

        public void ShowError(string message);

        public void ShowInfo(string message);
    }
}
