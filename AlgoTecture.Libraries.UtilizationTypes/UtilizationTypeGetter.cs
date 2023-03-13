using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Libraries.UtilizationTypes;

public class UtilizationTypeGetter : IUtilizationTypeGetter
{
    private readonly IUnitOfWork _unitOfWork;

    public UtilizationTypeGetter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UtilizationType>> GetAll()
    {
        return await _unitOfWork.UtilizationTypes.All();
    }
}