using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AlgoTecture.Libraries.Users.Models
{
    public class AuthenticationConstants
    {
        public const string Audience = "http://localhost:5000/";
        public const int LifeTimeMinutes = 43800;
        public const string PatternToValidatePassword = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";
        public const string PatternToValidateLogin = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}