using MusicRecognitionApp.Core.Enums;

namespace MusicRecognitionApp.Infrastructure.Data.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = RoleNames.User.ToString();

        public UserEntity(string username, string passwordHash, string role)
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
