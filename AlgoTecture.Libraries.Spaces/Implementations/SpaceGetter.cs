using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Models;
using Algotecture.Domain.Models.Dto;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.Spaces.Interfaces;
using Newtonsoft.Json;

namespace Algotecture.Libraries.Spaces.Implementations
{
    public class SpaceGetter : ISpaceGetter
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpaceGetter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Space?> GetByCoordinates(double latitude, double longitude)
        {
            var targetSpace = await _unitOfWork.Spaces.GetByCoordinates(latitude, longitude);

            return targetSpace;
        }

        public async Task<List<Space>> GetByType(int utilizationTypeId)
        {
            var targetSpaces = await _unitOfWork.Spaces.GetByType(utilizationTypeId);

            return targetSpaces;
        }

        public async Task<Space?> GetById(long spaceId)
        {
            return await _unitOfWork.Spaces.GetById(spaceId);
        }

        public async Task<SpaceWithProperty?> GetByIdWithProperty(long spaceId)
        {
            var targetSpaceWithProperty = new SpaceWithProperty();

            var space = await _unitOfWork.Spaces.GetById(spaceId);
            if (space == null) return null;

            var spaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(space.SpaceProperty);

            targetSpaceWithProperty.Id = space.Id;
            targetSpaceWithProperty.Latitude = space.Latitude;
            targetSpaceWithProperty.Longitude = space.Longitude;
            targetSpaceWithProperty.SpaceAddress = space.SpaceAddress;
            targetSpaceWithProperty.SpaceProperty = spaceProperty;
            targetSpaceWithProperty.UtilizationType = space.UtilizationType;
            targetSpaceWithProperty.UtilizationTypeId = space.UtilizationTypeId;

            return targetSpaceWithProperty;
        }
    }
}