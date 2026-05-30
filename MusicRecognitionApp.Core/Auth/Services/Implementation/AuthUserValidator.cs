using MusicRecognitionApp.Core.Auth.Services.Interfaces;
using MusicRecognitionApp.Core.Auth.Services.Models;
using System.Text.RegularExpressions;

namespace MusicRecognitionApp.Core.Auth.Services.Implementation
{
    public class AuthUserValidator : IAuthUserValidator
    {
        private static readonly Regex UsernameRegex = new Regex(@"^[a-zA-Z0-9]{3,20}$");
        private static readonly Regex PasswordRegex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)([a-zA-Z\d]{10,})$");

        public OperationResult ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return OperationResult.Fail("Username cannot be empty.");
            }

            if (!UsernameRegex.IsMatch(username))
            {
                return OperationResult.Fail("Username length must be 3-20 characters long and must contain at least one number and one letter.");
            }

            return OperationResult.Success();
        }

        public OperationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return OperationResult.Fail("Password cannot be empty.");
            }

            if (!PasswordRegex.IsMatch(password))
            {
                return OperationResult.Fail("Password must contain numbers and letters and must be longer than 10.");
            }

            return OperationResult.Success();
        }

        public OperationResult ValidateData(string username, string password)
        {
            var usernameRes = ValidateUsername(username);
            if (!usernameRes.IsSuccess)
                return usernameRes;

            var passwordRes = ValidatePassword(password);
            if (!passwordRes.IsSuccess)
                return passwordRes;

            return OperationResult.Success();
        }
    }
}
