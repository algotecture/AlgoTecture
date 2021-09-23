using System.Threading.Tasks;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Interfaces
{
    public interface ISubSpaceService
    {
        Task<Space> AddSubSpaceToSpace(AddSubSpaceModel addSubSpaceModel);
    }
}