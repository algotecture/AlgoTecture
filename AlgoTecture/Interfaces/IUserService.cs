using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;

namespace AlgoTecture.Interfaces
{
    public interface IUserService
    {
        Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel);
    }
}