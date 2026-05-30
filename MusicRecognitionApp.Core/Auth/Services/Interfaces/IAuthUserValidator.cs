using MusicRecognitionApp.Core.Auth.Services.Models;

namespace MusicRecognitionApp.Core.Auth.Services.Interfaces
{
    public interface IAuthUserValidator
    {
        OperationResult ValidateUsername(string username);
        OperationResult ValidatePassword(string password);
        OperationResult ValidateData(string username, string password);
    }
}
