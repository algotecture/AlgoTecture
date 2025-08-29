using System.Text.RegularExpressions;
using Algotecture.Libraries.Users.Interfaces;
using Algotecture.Libraries.Users.Models;

namespace Algotecture.Libraries.Users.Implementations
{
    public class UserCredentialsValidator : IUserCredentialsValidator
    {
        public bool IsValidUserName(string login)
        {
            return !string.IsNullOrEmpty(login) && Regex.IsMatch(login, AuthenticationConstants.PatternToValidateLogin);
        }

        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && Regex.IsMatch(password, AuthenticationConstants.PatternToValidatePassword);
        }
    }
}