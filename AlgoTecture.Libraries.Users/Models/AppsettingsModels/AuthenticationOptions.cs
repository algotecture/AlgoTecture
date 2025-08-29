namespace Algotecture.Libraries.Users.Models.AppsettingsModels
{
    public class AuthenticationOptions
    {
        public string JwtAlgotectureSecret { get; set; } = null!;

        public string JwtIssuer { get; set; } = null!;
    }
}