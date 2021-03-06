using System;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Interfaces;
using AlgoTecture.Persistence.Core.Interfaces;

namespace AlgoTecture.Implementations
{
    public class SpaceGetter : ISpaceGetter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceGetter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Space> GetByCoordinates(double latitude, double longitude)
        {
            var targetSpace = await _unitOfWork.Spaces.GetByCoordinates(latitude, longitude);

            return targetSpace;
        }
    }
}