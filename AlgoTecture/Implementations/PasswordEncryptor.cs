using System;
using System.Security.Cryptography;
using System.Text;
using AlgoTecture.Interfaces;

namespace AlgoTecture.Implementations
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        public string Encrypt(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            var provider = new SHA1CryptoServiceProvider();
            var byteHash = provider.ComputeHash(Encoding.Unicode.GetBytes(password));
            return Convert.ToBase64String(byteHash);
        } 
    }
}