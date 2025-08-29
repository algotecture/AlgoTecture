using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Algotecture.Domain.Models.Dto;
using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Libraries.Users.Interfaces
{
    public interface IBearerAuthenticator
    {
        Task<BearerTokenResponseModel> BearerAuthentication(UserCredentialModel userCredentialModel);

        ClaimsIdentity GetIdentity(User user);

        JwtSecurityToken GetJwtToken(ClaimsIdentity claimsIdentity);
    }
}