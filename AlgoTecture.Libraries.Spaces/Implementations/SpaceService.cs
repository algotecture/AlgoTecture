using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.Spaces.Models.Dto;
using Volo.Abp.DependencyInjection;

namespace AlgoTecture.Libraries.Spaces.Implementations;

public class SpaceService : ISpaceService, ITransientDependency
{
    public Task<Space> AddOrUpdateSpace(AddSpaceModel addSpaceModel)
    {
        return null;
    }
}