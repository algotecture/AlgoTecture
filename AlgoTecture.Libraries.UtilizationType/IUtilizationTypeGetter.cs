namespace AlgoTecture.Libraries.UtilizationType;

public interface IUtilizationTypeGetter
{
    Task<IEnumerable<Domain.Models.RepositoryModels.UtilizationType>> GetAll();
}