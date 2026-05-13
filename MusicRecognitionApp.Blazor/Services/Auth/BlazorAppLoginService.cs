using Microsoft.AspNetCore.Components.Authorization;
using MusicRecognitionApp.Infrastructure.Auth;

namespace MusicRecognitionApp.Blazor.Services.Auth
{
    public class BlazorAppLoginService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IUserService _userService;

        public BlazorAppLoginService(
            AuthenticationStateProvider authStateProvider, 
            IUserService userService)
        {
            _authStateProvider = authStateProvider;
            _userService = userService;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var (isValid, token) = await _userService.LoginAsync(username, password);
            
            if (isValid && !string.IsNullOrEmpty(token))
            {
                if(_authStateProvider is BlazorAuthStateProvider provider)
                    await provider.MarkUserAsAuthenticatedAsync(token);
                
                return true;
            }

            return false;
        }

        public async Task RegisterAsync(string username, string password)
        {
            await _userService.RegisterUserAsync(username, password);
        }

        public async Task LogoutAsync()
        {
            if (_authStateProvider is BlazorAuthStateProvider provider)
                await provider.MarkUserAsLoggedOutAsync();
        }
    }
}
