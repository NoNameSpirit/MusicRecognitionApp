using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Core.Models.Dto;

namespace MusicRecognitionApp.Core.Auth.Services.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public UserModel? User { get; }


        public OperationResult(bool isSuccess, string error, UserModel? user = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            User = user;
        }

        public static OperationResult Success()
            => new OperationResult(true, string.Empty);
        
        public static OperationResult SuccessWithUser(UserModel user)
            => new OperationResult(true, string.Empty, user);

        public static OperationResult Fail(string message)
            => new OperationResult(false, message);
    }
}
