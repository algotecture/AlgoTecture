using System.Threading.Tasks;
using AlgoTecture.Models.Dto;

namespace AlgoTecture.Interfaces
{
    public interface IUserService
    {
        Task<BearerTokenResponseModel> Create(UserCredentialModel userCredentialModel);
    }
}