namespace MusicRecognitionApp.Presentation
{
    public interface IApplicationForm
    {
        public void AddControl(UserControl control);

        public void RemoveControl(UserControl control);

        public void BringToFront(UserControl control);

        public void SetVisibility(UserControl control, bool visible);
    }
}
