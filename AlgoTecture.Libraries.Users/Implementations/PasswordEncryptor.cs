using System.Security.Cryptography;
using System.Text;
using Algotecture.Libraries.Users.Interfaces;

namespace Algotecture.Libraries.Users.Implementations
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        [Obsolete("Obsolete")]
        public string Encrypt(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            var provider = new SHA1CryptoServiceProvider();
            var byteHash = provider.ComputeHash(Encoding.Unicode.GetBytes(password));
            return Convert.ToBase64String(byteHash);
        } 
    }
}