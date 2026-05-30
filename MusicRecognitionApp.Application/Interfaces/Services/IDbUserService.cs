using MusicRecognitionApp.Core.Auth.Services.Models;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IDbUserService
    {
        Task<OperationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default);

        Task<OperationResult> RegisterAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
