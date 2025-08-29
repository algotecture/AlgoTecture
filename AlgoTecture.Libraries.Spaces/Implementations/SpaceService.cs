using System.Collections.Immutable;
using Algotecture.Data.Persistence.Core.Interfaces;
using Algotecture.Domain.Models;
using Algotecture.Domain.Models.RepositoryModels;
using Algotecture.Libraries.Spaces.Interfaces;
using Algotecture.Libraries.Spaces.Models.Dto;
using GeoDistanceLib;
using Newtonsoft.Json;

namespace Algotecture.Libraries.Spaces.Implementations;

public class SpaceService : ISpaceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistanceCalculator _distanceCalculator;

    public SpaceService(IUnitOfWork unitOfWork, IDistanceCalculator distanceCalculator)
    {
        _unitOfWork = unitOfWork;
        _distanceCalculator = distanceCalculator;
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
            Name = addSpaceModel.SpaceProperty?.Name,
            Description = addSpaceModel.SpaceProperty?.Description,
            Properties = addSpaceModel.SpaceProperty?.Properties,
            Images = new List<string>()
        };

        RecursiveFindAndAddGuidToSubSpace(addSpaceModel.SpaceProperty?.SubSpaces!);
        spaceProperty.SubSpaces = addSpaceModel.SpaceProperty?.SubSpaces;
        
        var serializedSpaceProperty = JsonConvert.SerializeObject(spaceProperty);
        
        var entityToInsert = new Space
        {
            Latitude = addSpaceModel.Latitude, Longitude = addSpaceModel.Longitude, SpaceAddress = addSpaceModel.SpaceAddress!,
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

        var spaceProperty = new SpaceProperty
        {
            SpacePropertyId = updateSpaceModel.SpaceProperty!.SpacePropertyId,
            Name = updateSpaceModel.SpaceProperty.Name,
            Description = updateSpaceModel.SpaceProperty.Description,
            Properties = updateSpaceModel.SpaceProperty.Properties,
            Images = updateSpaceModel.SpaceProperty.Images,
            SubSpaces = updateSpaceModel.SpaceProperty.SubSpaces
        };

        var serializedSpaceProperty = JsonConvert.SerializeObject(spaceProperty);
        
        var entityToUpdate = new Space
        {
            Latitude = updateSpaceModel.Latitude, Longitude = updateSpaceModel.Longitude, SpaceAddress = updateSpaceModel.SpaceAddress!,
            UtilizationTypeId = updateSpaceModel.UtilizationTypeId, SpaceProperty = serializedSpaceProperty, Id = updateSpaceModel.SpaceId
        };

        var updatedEntity = await _unitOfWork.Spaces.Upsert(entityToUpdate);
        await _unitOfWork.CompleteAsync();

        return updatedEntity;  
    }

    public async Task<List<KeyValuePair<int,Space>>> GetNearestSpaces(IEnumerable<Space> spaces, double latitude, double longitude, 
        int spaceCountToOut)
    {
        var list = new List<KeyValuePair<int,Space>>() ;
        foreach (var space in spaces)
        {
            var distance = _distanceCalculator.GetDistanceInKilometers(latitude, longitude,
                space.Latitude, space.Longitude);
            var distanceInMeters = Convert.ToInt32(distance * 1000);
            list.Add(new KeyValuePair<int, Space>(distanceInMeters, space));
        }

        if (!list.Any()) return list;

        var newSortedList = list.OrderBy(x=>x.Key).Take(spaceCountToOut).ToList();
        return newSortedList;
    }

    private static void RecursiveFindAndAddGuidToSubSpace(List<SubSpace> subSpaces)
    {
        for (var i = 0; i < subSpaces.Count; i++)
        {
            subSpaces[i].SubSpaceId = Guid.NewGuid();
            subSpaces[i].Images = new List<string>();
            
            RecursiveFindAndAddGuidToSubSpace(subSpaces[i].Subspaces!);
        }
    }
}