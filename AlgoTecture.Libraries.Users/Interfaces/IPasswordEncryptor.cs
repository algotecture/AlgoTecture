namespace AlgoTecture.Libraries.Users.Interfaces
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
    }
}