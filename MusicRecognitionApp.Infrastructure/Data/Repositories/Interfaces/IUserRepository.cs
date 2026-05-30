using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity userEntity, CancellationToken cancellationToken = default);
        Task<UserEntity> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<bool> IsUserExists(string username, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
