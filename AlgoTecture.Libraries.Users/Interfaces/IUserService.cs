using AlgoTecture.Domain.Models.Dto;

namespace AlgoTecture.Libraries.Users.Interfaces
{
    public interface IUserService
    {
        Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel);
    }
}