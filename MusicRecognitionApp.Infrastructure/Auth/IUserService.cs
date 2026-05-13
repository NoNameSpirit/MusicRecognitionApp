namespace MusicRecognitionApp.Infrastructure.Auth
{
    public interface IUserService
    {
        Task<(bool IsValid, string? Token)> LoginAsync(string username, string password);
        Task RegisterUserAsync(string username, string password);
    }
}
