using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.WebApi.Interfaces
{
    public interface ISubSpaceService
    {
        Task<Space> AddSubSpaceToSpace(AddSubSpaceModel addSubSpaceModel);
    }
}