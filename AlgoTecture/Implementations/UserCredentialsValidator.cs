using System.Text.RegularExpressions;
using AlgoTecture.Interfaces;
using AlgoTecture.Models;

namespace AlgoTecture.Implementations
{
    public class UserCredentialsValidator : IUserCredentialsValidator
    {
        public bool IsValidUserName(string login)
        {
            return !string.IsNullOrEmpty(login) && Regex.IsMatch(login, AuthenticationOptions.PatternToValidateLogin);
        }

        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && Regex.IsMatch(password, AuthenticationOptions.PatternToValidatePassword);
        }
    }
}