using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Libraries.UtilizationTypes;

public interface IUtilizationTypeGetter
{
    Task<IEnumerable<UtilizationType>> GetAll();
}