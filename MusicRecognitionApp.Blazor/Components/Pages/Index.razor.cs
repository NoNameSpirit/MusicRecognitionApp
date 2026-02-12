using Microsoft.AspNetCore.Components;
using MudBlazor;
using MusicRecognitionApp.Blazor.Components.Pages.Dialogs;

namespace MusicRecognitionApp.Blazor.Components.Pages
{
    partial class Index : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private async Task OpenAddTrackDialog()
        {
            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
            };

            await DialogService.ShowAsync<AddTrackDialog>("Add Track", options);
        }
    }
}
