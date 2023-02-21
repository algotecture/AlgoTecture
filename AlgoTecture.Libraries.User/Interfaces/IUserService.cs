using AlgoTecture.Domain.Models.Dto;

namespace AlgoTecture.Libraries.User.Interfaces
{
    public interface IUserService
    {
        Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel);
    }
}