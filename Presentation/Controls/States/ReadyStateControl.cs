using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using NAudio.Wave;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly IAudioRecognition _recognitionService;
        private readonly MainForm _mainForm;
        private readonly IAnimationService _animationService;
        private readonly IMessageBox _messageBox;

        private bool _isProcessing = false;
        
        public ReadyStateControl(
            IAudioRecognition recognitionService,
            MainForm mainForm,
            IAnimationService animationService,
            IMessageBox messageBox)
        {
            InitializeComponent();

            _recognitionService = recognitionService;
            _mainForm = mainForm;
            _animationService = animationService;
            _messageBox = messageBox;

            _animationService.AddHoverAnimation(PicRecordingGif);
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void BtnStartRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Recording);
        }

        private void PicRecordingGif_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Recording);
        }

        private async void FABtnAddingTracks_Click(object sender, EventArgs e)
        {
            if (_isProcessing)
            {
                return;
            }

            try
            {
                _isProcessing = true;

                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите папку содержащую музыку";
                    folderDialog.ShowNewFolderButton = false;

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string folderPath = folderDialog.SelectedPath;

                        var result = _messageBox.ShowQuestion($"Добавить все аудио файлы из папки: {folderPath}?");

                        if (result == DialogResult.Yes)
                        {
                            _mainForm.SetStateAsync(AppState.Processing);
                            //await Task.Yield();

                            var (added, failed, errors) = await _recognitionService.AddTracksFromFolderAsync(folderPath);

                            _mainForm.SetStateAsync(AppState.Ready);

                            var message = $"Добавлено треков в бд: {added}, не удалось добавить: {failed} {Environment.NewLine}";

                            if (errors.Any())
                            {
                                message += $"Ошибки:{Environment.NewLine}" +
                                           $"{string.Join($"{Environment.NewLine}", errors.Take(3))}";
                            }

                            _messageBox.ShowInfo(message);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _messageBox.ShowError($"Ошибка при импорте: {ex.Message}");
                _mainForm.SetStateAsync(AppState.Ready);
            }
            finally
            {
                _isProcessing = false;
            }
        }
    }
}
