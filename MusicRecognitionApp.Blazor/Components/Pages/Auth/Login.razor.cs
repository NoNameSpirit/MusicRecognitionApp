using Microsoft.AspNetCore.Components;
using MudBlazor;
using MusicRecognitionApp.Blazor.Services.Auth.Implementations;
using MusicRecognitionApp.Core.Auth.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Blazor.Components.Pages.Auth
{
    public partial class Login : CancellableComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private BlazorAppLoginService LoginService { get; set; } = null!;
        [Inject] private IAuthUserValidator UserValidator { get; set; } = null!;

        private UserDto _model = new UserDto();
        private string[] _errors = [];

        private async Task HandleLogin()
        {
            if (_errors.Length != 0)
                return;

            try
            {
                var result = await LoginService.LoginAsync(_model.Username, _model.Password);

                if (result.IsSuccess)
                {
                    Snackbar.Add($"Welcome, {result.User.Username}!", Severity.Success);
                    Navigation.NavigateTo("/");
                }
                else
                {
                    Snackbar.Add(result.Error, Severity.Error);
                } 
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Login error: {ex.Message}", Severity.Error);
            }
        }
    }
}
