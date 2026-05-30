using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Auth.Services.Interfaces;
using MusicRecognitionApp.Core.Auth.Services.Models;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class DbUserService : IDbUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthUserValidator _validator;
        private readonly IPasswordHasher _hasher;
        private readonly ILogger<DbUserService> _logger;
        public DbUserService(
            IUserRepository userRepository,
            IAuthUserValidator validator,
            IPasswordHasher hasher,
            ILogger<DbUserService> logger)
        {
            _userRepository = userRepository;
            _validator = validator;
            _hasher = hasher;
            _logger = logger;
        }

        public async Task<OperationResult> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(username, cancellationToken);

                if (user == null || !_hasher.Verify(password, user.PasswordHash))
                    return OperationResult.Fail("Invalid username or password.");

                return OperationResult.SuccessWithUser(EntityToModel.ToUserModel(user));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user by username '{username}'");
                return OperationResult.Fail($"Error getting user by username '{username}'");
            }
        }

        public async Task<OperationResult> RegisterAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = _validator.ValidateData(username, password);

                if (!result.IsSuccess)
                    return result;

                if (await _userRepository.IsUserExists(username, cancellationToken))
                    return OperationResult.Fail("Username already taken.");

                var hash = _hasher.HashPassword(password);
                var user = new UserEntity(username, hash, RoleNames.User.ToString());

                await _userRepository.AddAsync(user, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);

                return OperationResult.Success();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error registering  user by username '{username}'");
                return OperationResult.Fail($"Error registering user by username '{username}'");
            }
        }
    }
}