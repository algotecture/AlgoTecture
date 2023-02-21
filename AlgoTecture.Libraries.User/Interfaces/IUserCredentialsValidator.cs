namespace AlgoTecture.Libraries.User.Interfaces
{
    public interface IUserCredentialsValidator
    {
        bool IsValidUserName(string login);
        bool IsValidPassword(string password);
    }
}