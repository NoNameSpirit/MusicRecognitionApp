namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IMessageBox
    {
        public DialogResult ShowWarning(string message);

        public void ShowError(string message);
    }
}
