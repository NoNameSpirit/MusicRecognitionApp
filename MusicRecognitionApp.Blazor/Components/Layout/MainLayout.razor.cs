using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MusicRecognitionApp.Blazor.Services.Auth.Implementations;

namespace MusicRecognitionApp.Blazor.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private BlazorAppLoginService LoginService { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        protected override void OnInitialized()
        {
            AuthStateProvider.AuthenticationStateChanged += _
                => InvokeAsync(StateHasChanged);
        }

        private async Task Logout()
        {
            await LoginService.LogoutAsync();
            Navigation.NavigateTo("/");
        }

        private void Login()
        {
            Navigation.NavigateTo("/login");
        }
    }
}
