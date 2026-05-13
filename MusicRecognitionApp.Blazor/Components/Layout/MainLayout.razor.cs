using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MusicRecognitionApp.Blazor.Services.Auth;

namespace MusicRecognitionApp.Blazor.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = null!;
        [Inject] private BlazorAppLoginService LoginServic { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        protected override void OnInitialized()
        {
            AuthStateProvider.AuthenticationStateChanged += _
                => InvokeAsync(StateHasChanged); 
        }

        private async Task Logout()
        {
            await LoginServic.LogoutAsync();
            Navigation.NavigateTo("/");
        }

        private void Login()
        {
            Navigation.NavigateTo("/login");
        }
    }
}
