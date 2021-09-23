using System;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Interfaces;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Implementations
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