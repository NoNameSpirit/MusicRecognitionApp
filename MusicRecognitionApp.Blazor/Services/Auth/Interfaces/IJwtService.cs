using System.Security.Claims;

namespace MusicRecognitionApp.Blazor.Services.Auth.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);

        ClaimsPrincipal? ValidateToken(string token);
    }
}
