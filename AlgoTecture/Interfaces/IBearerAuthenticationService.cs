using System.Threading.Tasks;
using AlgoTecture.Models.Dto;

namespace AlgoTecture.Interfaces
{
    public interface IBearerAuthenticationService
    {
        Task<BearerTokenResponseModel> BearerAuthentication(UserCredentialModel userCredentialModel);
    }
}