using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Space.Models.Dto;

namespace AlgoTecture.Libraries.Space.Interfaces;

public interface ISpaceService
{
    Task<Domain.Models.RepositoryModels.Space> AddOrUpdateSpace(AddSpaceModel addSpaceModel);
}