namespace Algotecture.Libraries.Users.Interfaces
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
    }
}