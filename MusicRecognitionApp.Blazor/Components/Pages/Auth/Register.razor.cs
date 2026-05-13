using Microsoft.AspNetCore.Components;
using MudBlazor;
using MusicRecognitionApp.Blazor.Services.Auth;
using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Blazor.Components.Pages.Auth
{
    public partial class Register : CancellableComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private BlazorAppLoginService LoginService { get; set; } = null!;

        private UserDto _model = new UserDto();
        private string[] _errors = [];

        private async Task HandleRegister()
        {
            if (_errors.Length != 0)
                return;

            try
            {
                await LoginService.RegisterAsync(_model.Username, _model.Password);
                Snackbar.Add("Registration successful! Please login.", Severity.Success);
                Navigation.NavigateTo("/login");
            }
            catch (InvalidOperationException ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            catch (Exception)
            {
                Snackbar.Add("Registration error", Severity.Error);
            }
        }

        private IEnumerable<string> ValidationUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                yield return "Username is required";

            if (username.Length < 5)
                yield return "Username must be greater than 5 symbols";
        }

        private IEnumerable<string> ValidationPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                yield return "Password is required";
        }
    }
}
