using Algotecture.Domain.Models.Dto;

namespace Algotecture.Libraries.Users.Interfaces
{
    public interface IUserService
    {
        Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel);
    }
}