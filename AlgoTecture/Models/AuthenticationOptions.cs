using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AlgoTecture.Models
{
    public class AuthenticationOptions
    {
        public const string Issuer = "AlgoTectureAuthenticationServer";
        public const string Audience = "http://localhost:5000/";
        const string Key = "test";
        public static SymmetricSecurityKey SymmetricSecurityKey { get; set; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        public const int LifeTimeMinutes = 43800;
        public const string PatternToValidatePassword = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";
        public const string PatternToValidateLogin = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";
        //public const string PatternToValidateName = @"^[a-zA-ZА-Яа-я]{1,20}";
        //public const string AccessTokenAlgoTecture = "accessTokenAlgoTecture";
    }
}