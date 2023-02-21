using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AlgoTecture.Domain.Models.Dto;

namespace AlgoTecture.Libraries.User.Interfaces
{
    public interface IBearerAuthenticator
    {
        Task<BearerTokenResponseModel> BearerAuthentication(UserCredentialModel userCredentialModel);

        ClaimsIdentity GetIdentity(Domain.Models.RepositoryModels.User user);

        JwtSecurityToken GetJwtToken(ClaimsIdentity claimsIdentity);
    }
}