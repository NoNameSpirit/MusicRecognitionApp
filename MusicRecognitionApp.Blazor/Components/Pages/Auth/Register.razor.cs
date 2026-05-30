using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using MusicRecognitionApp.Blazor.Services.Auth.Implementations;
using MusicRecognitionApp.Core.Auth.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Blazor.Components.Pages.Auth
{
    public partial class Register : CancellableComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private BlazorAppLoginService LoginService { get; set; } = null!;
        [Inject] private IAuthUserValidator UserValidator { get; set; } = null!;

        private UserDto _model = new UserDto();
        private string[] _errors = [];

        private async Task HandleRegister()
        {
            if (_errors.Length != 0)
                return;

            try
            {
                var result = await LoginService.RegisterAsync(_model.Username, _model.Password);

                if (result.IsSuccess)
                {
                    Snackbar.Add($"Registration successful! Please login.", Severity.Success);
                    Navigation.NavigateTo("/login");
                }
                else 
                {
                    Snackbar.Add(result.Error, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Registration error: {ex.Message}", Severity.Error);
            }
        }

        private IEnumerable<string> ValidationUsername(string username)
        {
            var result = UserValidator.ValidateUsername(username);
            if (!result.IsSuccess)
            {
                yield return result.Error;
            }
        }

        private IEnumerable<string> ValidationPassword(string password)
        {
            var result = UserValidator.ValidatePassword(password);
            if (!result.IsSuccess)
            {
                yield return result.Error;
            }
        }
    }
}
