using System.Text.RegularExpressions;
using AlgoTecture.Libraries.Users.Interfaces;
using AlgoTecture.Libraries.Users.Models;

namespace AlgoTecture.Libraries.Users.Implementations
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