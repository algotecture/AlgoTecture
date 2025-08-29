using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Libraries.UtilizationTypes;

public interface IUtilizationTypeGetter
{
    Task<IEnumerable<UtilizationType>> GetAll();
}