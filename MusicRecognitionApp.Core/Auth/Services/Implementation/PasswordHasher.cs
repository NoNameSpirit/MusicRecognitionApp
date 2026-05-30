using MusicRecognitionApp.Core.Auth.Services.Interfaces;

namespace MusicRecognitionApp.Core.Auth.Services.Implementation
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}