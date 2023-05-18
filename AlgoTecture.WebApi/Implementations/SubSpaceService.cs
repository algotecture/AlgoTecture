using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.WebApi.Interfaces;
using Newtonsoft.Json;

namespace AlgoTecture.WebApi.Implementations
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
            if (addSubSpaceModel == null) throw new ArgumentNullException(nameof(addSubSpaceModel));
            
            var targetSpace = await _unitOfWork.Spaces.GetById(addSubSpaceModel.SpaceId);
            
            if (targetSpace == null) throw new ArgumentNullException(nameof(targetSpace));
            
            var targetSpaceProperty = JsonConvert.DeserializeObject<SpaceProperty>(targetSpace.SpaceProperty);

            if (targetSpaceProperty == null) throw new ArgumentNullException(nameof(targetSpaceProperty));

            var targetSubSpaces = targetSpaceProperty.SubSpaces;

            RecursiveFindAndAddSubSpace(targetSubSpaces, addSubSpaceModel.SubSpaceIdToUpdate, addSubSpaceModel.SubSpace);

            targetSpaceProperty.SubSpaces = targetSubSpaces;

            var serializedSpaceProperty = JsonConvert.SerializeObject(targetSpaceProperty);

            targetSpace.SpaceProperty = serializedSpaceProperty;
            
            var updatedSpace = await _unitOfWork.Spaces.Upsert(targetSpace);
            
            await _unitOfWork.CompleteAsync();

            return updatedSpace;
        }

        private static void RecursiveFindAndAddSubSpace(List<SubSpace> subSpaces, Guid subSpaceId, SubSpace newSubSpace)
        {
            for (var i = 0; i < subSpaces.Count; i++)
            {
                if (subSpaces[i].SubSpaceId == subSpaceId)
                {
                    if (subSpaces[i].Subspaces != null)
                        subSpaces[i].Subspaces.Add(newSubSpace);
                    return;
                }
                RecursiveFindAndAddSubSpace(subSpaces[i].Subspaces, subSpaceId, newSubSpace);
            }
        }
    }
}