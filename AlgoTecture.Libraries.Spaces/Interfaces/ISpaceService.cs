using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Models.Dto;

namespace AlgoTecture.Libraries.Spaces.Interfaces;

public interface ISpaceService
{
    Task<Space> AddSpace(AddSpaceModel addSpaceModel);
}