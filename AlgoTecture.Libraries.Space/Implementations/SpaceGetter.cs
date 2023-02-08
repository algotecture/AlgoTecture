using AlgoTecture.Libraries.Space.Interfaces;
using AlgoTecture.Persistence.Core.Interfaces;

namespace AlgoTecture.Libraries.Space.Implementations
{
    public class SpaceGetter : ISpaceGetter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceGetter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Domain.Models.RepositoryModels.Space> GetByCoordinates(double latitude, double longitude)
        {
            var targetSpace = await _unitOfWork.Spaces.GetByCoordinates(latitude, longitude);

            return targetSpace;
        }
        
        public async Task<List<Domain.Models.RepositoryModels.Space>> GetByType(int utilizationTypeId)
        {
            var targetSpaces = await _unitOfWork.Spaces.GetByType(utilizationTypeId);

            return targetSpaces;
        }
        
        public async Task<Domain.Models.RepositoryModels.Space> GetById(long spaceId)
        {
            return await _unitOfWork.Spaces.GetById(spaceId);
        }
    }
}