using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class SongAddingService : ISongAddingService
    {
        private readonly IAudioRecognitionService _recognitionService;
        private readonly IMessageBox _messageBox;

        public SongAddingService(
            IAudioRecognitionService recognitionService,
            IMessageBox messageBox)
        {
            _recognitionService = recognitionService;
            _messageBox = messageBox;
        }

        public async Task<ImportResult> ImportTracksFromFolderAsync()
        {
            using var folderDialog = new FolderBrowserDialog()
            {
                Description = "Выберите папку содержащую музыку",
                ShowNewFolderButton = false,
            };

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                return new ImportResult(false, "Отменено пользователем");
            }

            string folderPath = folderDialog.SelectedPath;

            var result = _messageBox.ShowQuestion($"Добавить все аудио файлы из папки: {folderPath}?");

            if (result != DialogResult.Yes)
            {
                return new ImportResult(false, "Отменено пользователем");
            }

            try
            {
                var (added, failed, errors) = await _recognitionService.AddTracksFromFolderAsync(folderPath);

                var message = $"Добавлено треков в БД: {added}, не удалось добавить: {failed}";
                if (errors.Any())
                {
                    message += $"{Environment.NewLine}Ошибки:{Environment.NewLine}{string.Join($"{Environment.NewLine}", errors.Take(3))}";
                }

                return new ImportResult(true, message, added, failed);
            }
            catch (Exception ex)
            {
                return new ImportResult(false, $"Ошибка при импорте: {ex.Message}");
            }
        }
    }
}
