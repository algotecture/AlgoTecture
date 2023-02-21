namespace AlgoTecture.Libraries.User.Interfaces
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
    }
}