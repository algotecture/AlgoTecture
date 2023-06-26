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
            Properties = addSpaceModel.SpaceProperty.Properties,
            Images = new List<string>()
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

    public async Task<Space> UpdateSpace(UpdateSpaceModel updateSpaceModel) 
    {
        var targetSpace = await _unitOfWork.Spaces.GetById(updateSpaceModel.SpaceId);

        if (targetSpace == null)
        {
            throw new InvalidOperationException($"Space with id = {updateSpaceModel.SpaceId} not found");
        }

        var spaceProperty = new SpaceProperty()
        {
            SpacePropertyId = updateSpaceModel.SpaceProperty.SpacePropertyId,
            Name = updateSpaceModel.SpaceProperty.Name,
            Description = updateSpaceModel.SpaceProperty.Description,
            Properties = updateSpaceModel.SpaceProperty.Properties,
            Images = updateSpaceModel.SpaceProperty.Images,
            SubSpaces = updateSpaceModel.SpaceProperty.SubSpaces
        };

        var serializedSpaceProperty = JsonConvert.SerializeObject(spaceProperty);
        
        var entityToUpdate = new Space
        {
            Latitude = updateSpaceModel.Latitude, Longitude = updateSpaceModel.Longitude, SpaceAddress = updateSpaceModel.SpaceAddress,
            UtilizationTypeId = updateSpaceModel.UtilizationTypeId, SpaceProperty = serializedSpaceProperty, Id = updateSpaceModel.SpaceId
        };

        var updatedEntity = await _unitOfWork.Spaces.Upsert(entityToUpdate);
        await _unitOfWork.CompleteAsync();

        return updatedEntity;  
    }

    private static void RecursiveFindAndAddGuidToSubSpace(List<SubSpace> subSpaces)
    {
        for (var i = 0; i < subSpaces.Count; i++)
        {
            subSpaces[i].SubSpaceId = Guid.NewGuid();
            subSpaces[i].Images = new List<string>();
            
            RecursiveFindAndAddGuidToSubSpace(subSpaces[i].Subspaces);
        }
    }
}