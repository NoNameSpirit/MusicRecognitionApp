using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class SongAddingService : ISongAddingService
    {
        private readonly IProcessingAudio _processingAudio;
        private readonly IMessageBox _messageBox;

        public SongAddingService(
            IProcessingAudio processingAudio,
            IMessageBox messageBox)
        {
            _processingAudio = processingAudio;
            _messageBox = messageBox;
        }

        public async Task<ImportResult> ImportTracksFromFolderAsync()
        {
            using var folderDialog = new FolderBrowserDialog()
            {
                Description = "Select the folder containing the music.",
                ShowNewFolderButton = false,
            };

            if (folderDialog.ShowDialog() != DialogResult.OK)
            {
                return new ImportResult(false, "Cancelled by user");
            }

            string folderPath = folderDialog.SelectedPath;

            var result = _messageBox.ShowQuestion($"Add all audio files from a folder: {folderPath}?");

            if (result != DialogResult.Yes)
            {
                return new ImportResult(false, "Cancelled by user");
            }

            try
            {
                var (added, failed, errors) = await Task.Run(() => _processingAudio.AddTracksFromFolderAsync(folderPath));

                var message = $"Tracks added to the database: {added}, couldn't add: {failed}";
                if (errors.Any())
                {
                    message += $"{Environment.NewLine}Errors:{Environment.NewLine}{string.Join($"{Environment.NewLine}", errors.Take(3))}";
                }

                return new ImportResult(true, message, added, failed);
            }
            catch (Exception ex)
            {
                return new ImportResult(false, $"Error during import: {ex.Message}");
            }
        }
    }
}
