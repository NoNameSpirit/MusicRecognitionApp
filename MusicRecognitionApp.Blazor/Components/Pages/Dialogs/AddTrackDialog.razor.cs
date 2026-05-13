using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MusicRecognitionApp.Application.Services.Interfaces; 

namespace MusicRecognitionApp.Blazor.Components.Pages.Dialogs
{
    public partial class AddTrackDialog : CancellableComponentBase
    {
        private const int oneMB = 1024 * 1024;
        private const int maxAmountOfMB = 80;
        private static readonly HashSet<string> allowedExtensions = new HashSet<string> { ".mp3", ".wav" };

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private IProcessingAudio ProcessingAudio { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        private IBrowserFile? _selectedFile;
        private bool isProcessing;

        private void OnFileSelected(IBrowserFile file)
        {
            if (file.Size > oneMB * maxAmountOfMB)
            {
                Snackbar.Add($"File too large (max {maxAmountOfMB} MB)", Severity.Error);
                return;
            }

            var extension = Path.GetExtension(file.Name);
            if (!allowedExtensions.Contains(extension))
            {
                Snackbar.Add($"Unsupported extension: {extension}. Please select .wav or .mp3");
                return;
            }

            _selectedFile = file;
            StateHasChanged();
        }

        private async Task Add()
        {
            if (_selectedFile == null)
                return;

            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_" + _selectedFile.Name);

            try
            {
                isProcessing = true;
                StateHasChanged();

                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (!user.IsInRole("Admin"))
                {
                    Snackbar.Add("Access Denied: Admins only.", Severity.Error);
                    return;
                }
                    
                using (var stream = _selectedFile.OpenReadStream(oneMB * maxAmountOfMB))
                using (var fs = new FileStream(tempPath, FileMode.Create))
                {
                    await stream.CopyToAsync(fs, Ct);
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

                await ProcessingAudio.AddTrackAsync(tempPath, title, artist, Ct);

                Snackbar.Add($"Track '{artist} - {title}' added successfully!", Severity.Success);
                MudDialog.Close();
            }
            catch (OperationCanceledException)
            {
                Snackbar.Add("Upload cancelled.", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error: {ex.Message}", Severity.Error);
            }
            finally
            {
                isProcessing = false;
                StateHasChanged();

                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}