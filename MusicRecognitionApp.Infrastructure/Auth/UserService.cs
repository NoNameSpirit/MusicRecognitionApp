using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Auth
{
    public class UserService : IUserService
    {
        private readonly MusicRecognitionContext _context;
        private readonly IJwtService _jwtService;

        public UserService(
            MusicRecognitionContext context,
            IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<(bool IsValid, string? Token)> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return (false, null);

            string token = _jwtService.GenerateToken(user.Username, user.Role);

            return (true, token);
        }

        public async Task RegisterUserAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(e => e.Username == username))
                throw new InvalidOperationException("This Username already exists");

            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new UserEntity()
            {
                Username = username,
                PasswordHash = hash,
                Role = "User"
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
