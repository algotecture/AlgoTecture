using AlgoTecture.Libraries.Space.Interfaces;
using AlgoTecture.Libraries.Space.Models.Dto;
using Volo.Abp.DependencyInjection;

namespace AlgoTecture.Libraries.Space.Implementations;

public class SpaceService : ISpaceService, ITransientDependency
{
    Task<Domain.Models.RepositoryModels.Space> AddOrUpdateSpace(AddSpaceModel addSpaceModel)
    {
        
    }
}