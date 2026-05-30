using MusicRecognitionApp.Core.Auth.Services.Models;

namespace MusicRecognitionApp.Blazor.Services.Auth.Interfaces
{
    public interface IBlazorAppLoginService
    {
        Task<OperationResult> LoginAsync(string username, string password);
        Task<OperationResult> RegisterAsync(string username, string password);
        Task LogoutAsync();
    }
}
