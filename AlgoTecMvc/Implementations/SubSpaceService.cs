using System;
using System.Threading.Tasks;
using AlgoTecMvc.Core.Interfaces;
using AlgoTecMvc.Interfaces;
using AlgoTecMvc.Models;
using AlgoTecMvc.Models.Dto;
using Newtonsoft.Json;

namespace AlgoTecMvc.Implementations
{
    public class SubSpaceService : ISubSpaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubSpaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<SubSpace> AddSubSpace(AddSubSpaceModel addSubSpaceModel)
        {
            var targetSpace = await _unitOfWork.Spaces.GetById(addSubSpaceModel.SpaceId);
            
            if (targetSpace == null) throw new ArgumentNullException(nameof(targetSpace));
            
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

            if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));

            var targetSubSpace = targetSpaceProperty.SubSpaces[0];

            var subSpaceToUpdate = Find(targetSubSpace, addSubSpaceModel.SubSpaceId);
            
            return null;
        }
        
        private static SubSpace Find(SubSpace node, Guid subSpaceId)
        {
            if (node == null)
                return null;

            if (node.SubSpaceId == subSpaceId)
                return node;

            foreach (var child in node.Subspaces)
            {
                var found = Find(child, subSpaceId);
                if (found != null)
                    return found;
            }

            return null;
        }
    }
}