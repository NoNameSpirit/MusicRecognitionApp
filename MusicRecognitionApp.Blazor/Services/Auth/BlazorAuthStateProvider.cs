using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using MusicRecognitionApp.Infrastructure.Auth;

namespace MusicRecognitionApp.Blazor.Services.Auth
{
    public class BlazorAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IBrowserStorageService _storage;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlazorAuthStateProvider(
            IBrowserStorageService storage,
            IJwtService jwtService,
            IHttpContextAccessor httpContextAccessor)
        {
            _storage = storage;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string? token = null;

            var cookieToken = _httpContextAccessor.HttpContext?.Request.Cookies["MusicApp_Token"];

            if (!string.IsNullOrEmpty(cookieToken))
            {
                token = cookieToken;
            }
            else
            {
                try
                {
                    token = await _storage.GetAsync<string>("auth_token");
                }
                catch (InvalidOperationException)
                {
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                var principal = _jwtService.ValidateToken(token);
                if (principal != null)
                {
                    return new AuthenticationState(principal);
                }
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task MarkUserAsAuthenticatedAsync(string token)
        {
            var principal = _jwtService.ValidateToken(token);
            if (principal == null) 
                return;

            await _storage.SetAsync("auth_token", token);
            await _storage.SetCookieAsync("MusicApp_Token", token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _storage.RemoveAsync("auth_token");
            await _storage.RemoveCookieAsync("MusicApp_Token");

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}