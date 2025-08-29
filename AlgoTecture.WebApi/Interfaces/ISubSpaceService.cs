using System.Threading.Tasks;
using Algotecture.Domain.Models.Dto;
using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.WebApi.Interfaces
{
    public interface ISubSpaceService
    {
        Task<Space> AddSubSpaceToSpace(AddSubSpaceModel addSubSpaceModel);
    }
}