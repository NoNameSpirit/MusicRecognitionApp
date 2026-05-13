using System.Security.Claims;

namespace MusicRecognitionApp.Infrastructure.Auth
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);

        ClaimsPrincipal? ValidateToken(string token);
    }
}