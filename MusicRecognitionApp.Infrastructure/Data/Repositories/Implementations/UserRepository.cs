using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations
{
    public class UserRepository : RepositoryCrud<UserEntity>, IUserRepository
    {
        public UserRepository(MusicRecognitionContext context)
            : base(context)
        {
        }

        public async Task<bool> IsUserExists(string username, CancellationToken cancellationToken = default)
            => await IsExists(e => e.Username == username, cancellationToken);

        public async Task AddAsync(UserEntity userEntity, CancellationToken cancellationToken = default)
            => await InsertAsync(userEntity, cancellationToken);

        public async Task<UserEntity> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var userEntity = await GetAsync(
                filter: u => u.Username == username,
                cancellationToken: cancellationToken);

            return userEntity.FirstOrDefault();
        }
    }
}
