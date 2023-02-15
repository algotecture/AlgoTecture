using AlgoTecture.Persistence.Core.Interfaces;

namespace AlgoTecture.Libraries.UtilizationType;

public class UtilizationTypeGetter : IUtilizationTypeGetter
{
    private readonly IUnitOfWork _unitOfWork;

    public UtilizationTypeGetter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Domain.Models.RepositoryModels.UtilizationType>> GetAll()
    {
        return await _unitOfWork.UtilizationTypes.All();
    }
}