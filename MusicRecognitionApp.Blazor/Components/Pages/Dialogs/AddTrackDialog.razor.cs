using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Blazor.Components.Pages.Dialogs
{
    partial class AddTrackDialog : ComponentBase
    {
        private const int oneMB = 1024 * 1024;
        private const int maxAmountOfMB = 80;
        private static readonly HashSet<string> allowedExtensions = new HashSet<string> { ".mp3", ".wav" };

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private IProcessingAudio ProcessingAudio { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        private IBrowserFile? _selectedFile;
        private string? tempPath;
        private bool isProcessing;
        private CancellationTokenSource _cts;

        private void OnFileSelected(IBrowserFile file)
        {
            if (file.Size > oneMB * maxAmountOfMB)
            {
                Snackbar.Add($"File too large (max {maxAmountOfMB} MB)", Severity.Error);
                return;
            }

            ExtensionIsCorrect(file);
            
            _selectedFile = file;
            StateHasChanged();
        }

        private bool ExtensionIsCorrect(IBrowserFile file)
        {
            var extension = Path.GetExtension(file.Name);
            if (!allowedExtensions.Contains(extension))
            {
                Snackbar.Add($"Unsupported extension: {extension}. Please select .wav or .mp3");
                return false;
            }

            return true;
        }

        private async Task Add()
        {
            if (_selectedFile == null)
                return;

            if (!ExtensionIsCorrect(_selectedFile))
                return;

            try
            {
                isProcessing = true;
                StateHasChanged();

                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_" + _selectedFile.Name);
                using var stream = _selectedFile.OpenReadStream(oneMB * maxAmountOfMB, token);
                using (var fileStream = File.Create(tempPath))
                {
                    await stream.CopyToAsync(fileStream, token);
                }

                var fileName = Path.GetFileNameWithoutExtension(_selectedFile.Name);
                var artist = "Unknown Artist";
                var title = fileName;

                if (fileName.Contains(" - "))
                {
                    var parts = fileName.Split(" - ", 2);
                    artist = parts[0].Trim();
                    title = parts[1].Trim();
                }

                await ProcessingAudio.AddTrackAsync(tempPath, title, artist, token);

                Snackbar.Add($"Track Artist - Title: {artist} - {title} successfully added", Severity.Success);
                MudDialog.Close();
            }
            catch (OperationCanceledException) 
            {
                Snackbar.Add("Operation cancelled", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error: {ex.Message}", Severity.Error);
            }
            finally
            {
                isProcessing = false;
                _cts?.Dispose();
                _cts = null;
                StateHasChanged();

                if (!string.IsNullOrEmpty(tempPath) && File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }

        private void Cancel()
        {
            _cts?.Cancel();
            MudDialog.Cancel();
        }
    }
}
