using Microsoft.AspNetCore.Components.Authorization;
using MusicRecognitionApp.Blazor.Services.Auth.Implementations;
using MusicRecognitionApp.Blazor.Services.Auth.Interfaces;
using MusicRecognitionApp.Core.Models.Auth;

namespace MusicRecognitionApp.Blazor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddHttpContextAccessor()
                    .AddAuthorizationCore()
                    .AddAuthentication().AddCookie("CookieAuth", config =>
                    {
                        config.Cookie.Name = "MusicApp.Auth";
                        config.LoginPath = "/login";
                        config.AccessDeniedPath = "/";
                    });

            services.AddSingleton<IJwtService, JwtService>()
                    .AddScoped<IBrowserStorageService, BrowserStorageService>()
                    .AddScoped<AuthenticationStateProvider, BlazorAuthStateProvider>()
                    .AddScoped<BlazorAppLoginService>();

            return services;
        }
    }
}
