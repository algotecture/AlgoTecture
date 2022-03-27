using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface IBearerAuthenticator
    {
        Task<BearerTokenResponseModel> BearerAuthentication(UserCredentialModel userCredentialModel);

        ClaimsIdentity GetIdentity(User user);

        JwtSecurityToken GetJwtToken(ClaimsIdentity claimsIdentity);
    }
}