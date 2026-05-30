    using Microsoft.AspNetCore.Components.Authorization;
    using MusicRecognitionApp.Application.Interfaces.Services;
    using MusicRecognitionApp.Blazor.Services.Auth.Interfaces;
using MusicRecognitionApp.Core.Auth.Services.Models;

    namespace MusicRecognitionApp.Blazor.Services.Auth.Implementations
    {
        public class BlazorAppLoginService : IBlazorAppLoginService
        {
            private readonly AuthenticationStateProvider _authStateProvider;
            private readonly IDbUserService _dbUserService;
            private readonly IJwtService _jwtService;

            public BlazorAppLoginService(
                AuthenticationStateProvider authStateProvider,
                IDbUserService userService,
                IJwtService jwtService)
            {
                _authStateProvider = authStateProvider;
                _dbUserService = userService;
                _jwtService = jwtService;
            }

            public async Task<OperationResult> LoginAsync(string username, string password)
            {
                var result = await _dbUserService.LoginAsync(username, password);

                if (!result.IsSuccess || result.User == null)
                    return OperationResult.Fail(result.Error);

                string token = _jwtService.GenerateToken(result.User.Username, result.User.Role);

                if (_authStateProvider is BlazorAuthStateProvider provider)
                    await provider.MarkUserAsAuthenticatedAsync(token);

                return OperationResult.SuccessWithUser(result.User);
            }

            public async Task<OperationResult> RegisterAsync(string username, string password)
                => await _dbUserService.RegisterAsync(username, password);

            public async Task LogoutAsync()
            {
                if (_authStateProvider is BlazorAuthStateProvider provider)
                    await provider.MarkUserAsLoggedOutAsync();
            }
        }
    }
