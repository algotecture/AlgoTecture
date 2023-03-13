using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Interfaces;

namespace AlgoTecture.Libraries.Spaces.Implementations
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
        
        public async Task<List<Space>> GetByType(int utilizationTypeId)
        {
            var targetSpaces = await _unitOfWork.Spaces.GetByType(utilizationTypeId);

            return targetSpaces;
        }
        
        public async Task<Space> GetById(long spaceId)
        {
            return await _unitOfWork.Spaces.GetById(spaceId);
        }
    }
}