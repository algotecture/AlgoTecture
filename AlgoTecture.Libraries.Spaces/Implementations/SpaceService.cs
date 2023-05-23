using AlgoTecture.Data.Persistence.Core.Interfaces;
using AlgoTecture.Domain.Models;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.Spaces.Models.Dto;
using Newtonsoft.Json;

namespace AlgoTecture.Libraries.Spaces.Implementations;

public class SpaceService : ISpaceService
{
    private readonly IUnitOfWork _unitOfWork;

    public SpaceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Space> AddSpace(AddSpaceModel addSpaceModel)
    {
        var targetSpace = await _unitOfWork.Spaces.GetByCoordinates(addSpaceModel.Latitude, addSpaceModel.Longitude);

        if (targetSpace != null)
        {
            throw new InvalidOperationException($"Space with coordinates {addSpaceModel.Latitude},{addSpaceModel.Longitude} already exists");
        }

        var spaceProperty = new SpaceProperty()
        {
            SpacePropertyId = Guid.NewGuid(),
            Name = addSpaceModel.SpaceProperty.Name,
            Description = addSpaceModel.SpaceProperty.Description,
            OwnerId = 0,
            ContractId = 0,
            Properties = addSpaceModel.SpaceProperty.Properties,
        };

        RecursiveFindAndAddGuidToSubSpace(addSpaceModel.SpaceProperty.SubSpaces);
        spaceProperty.SubSpaces = addSpaceModel.SpaceProperty.SubSpaces;
        
        var serializedSpaceProperty = JsonConvert.SerializeObject(spaceProperty);
        
        var entityToInsert = new Space
        {
            Latitude = addSpaceModel.Latitude, Longitude = addSpaceModel.Longitude, SpaceAddress = addSpaceModel.SpaceAddress,
            UtilizationTypeId = addSpaceModel.UtilizationTypeId, SpaceProperty = serializedSpaceProperty
        };

        var insertedEntity = await _unitOfWork.Spaces.Add(entityToInsert);
        await _unitOfWork.CompleteAsync();

        return insertedEntity;
    }
    
    private static void RecursiveFindAndAddGuidToSubSpace(List<SubSpace> subSpaces)
    {
        for (var i = 0; i < subSpaces.Count; i++)
        {
            if (subSpaces[i].SubSpaceId == Guid.Empty)
            {
                subSpaces[i].SubSpaceId = Guid.NewGuid();
            }
            RecursiveFindAndAddGuidToSubSpace(subSpaces[i].Subspaces);
        }
    }
}