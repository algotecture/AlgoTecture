using System.Threading.Tasks;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface ISubSpaceService
    {
        Task<Space> AddSubSpaceToSpace(AddSubSpaceModel addSubSpaceModel);
    }
}