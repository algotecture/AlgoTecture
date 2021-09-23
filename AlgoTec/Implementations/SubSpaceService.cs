using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoTec.Core.Interfaces;
using AlgoTec.Interfaces;
using AlgoTec.Models;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;
using Newtonsoft.Json;

namespace AlgoTec.Implementations
{
    public class SubSpaceService : ISubSpaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubSpaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Space> AddSubSpaceToSpace(AddSubSpaceModel addSubSpaceModel)
        {
            var targetSpace = await _unitOfWork.Spaces.GetById(addSubSpaceModel.SpaceId);
            
            if (targetSpace == null) throw new ArgumentNullException(nameof(targetSpace));
            
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

            if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));

            var targetSubSpaces = targetSpaceProperty.SubSpaces;

            RecursiveFindAndUpdateTargetSubSpace(targetSubSpaces, addSubSpaceModel.SubSpaceId, addSubSpaceModel.SubSpace);

            targetSpaceProperty.SubSpaces = targetSubSpaces;

            var serializedSpaceProperty = JsonConvert.SerializeObject(targetSpaceProperty);

            targetSpace.SpaceProperty = serializedSpaceProperty;
            
            var updatedSpace = await _unitOfWork.Spaces.Upsert(targetSpace);
            
            await _unitOfWork.CompleteAsync();

            return updatedSpace;
        }

        private static void RecursiveFindAndUpdateTargetSubSpace(List<SubSpace> subSpaces, Guid subSpaceId, SubSpace newSubSpace)
        {
            for (var i = 0; i < subSpaces.Count; i++)
            {
                if (subSpaces[i].SubSpaceId == subSpaceId)
                {
                    subSpaces[i].Subspaces = new List<SubSpace> {newSubSpace};
                    return;
                }
                RecursiveFindAndUpdateTargetSubSpace(subSpaces[i].Subspaces, subSpaceId, newSubSpace);
            }
        }
    }
}