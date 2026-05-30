using MusicRecognitionApp.Core.Auth.Services.Models;

namespace MusicRecognitionApp.Application.Services.Auth
{
    public interface IUserService
    {
        Task<OperationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);
        Task<OperationResult> RegisterAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
