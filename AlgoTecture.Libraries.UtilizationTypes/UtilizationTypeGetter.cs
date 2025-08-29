using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Models.RepositoryModels;

namespace Algotecture.Libraries.UtilizationTypes;

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