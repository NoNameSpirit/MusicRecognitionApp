using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Auth.Services.Models;

namespace MusicRecognitionApp.Application.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IDbUserService _dbUserService;
        public UserService(IDbUserService dbUserService)
        {
            _dbUserService = dbUserService;
        }

        public async Task<OperationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
            => await _dbUserService.LoginAsync(username, password, cancellationToken);

        public async Task<OperationResult> RegisterAsync(string username, string password, CancellationToken cancellationToken = default)
            => await _dbUserService.RegisterAsync(username, password, cancellationToken);
    }
}
