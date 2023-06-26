using System.ComponentModel.DataAnnotations;
using AlgoTecture.Data.Images.Interfaces;
using AlgoTecture.Data.Images.Models;
using AlgoTecture.Libraries.Environments;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.Spaces.Models.Dto;

namespace AlgoTecture.Libraries.Spaces.Implementations;

public class SpaceImageService : ISpaceImageService
{
    private readonly ISpaceGetter _spaceGetter;
    private readonly IImageUploader _imageUploader;
    private readonly ISpaceService _spaceService;
    private readonly IImageGetter _imageGetter;

    public SpaceImageService(ISpaceGetter spaceGetter, IImageUploader imageUploader, ISpaceService spaceService, IImageGetter imageGetter)
    {
        _spaceGetter = spaceGetter;
        _imageUploader = imageUploader;
        _spaceService = spaceService;
        _imageGetter = imageGetter;
    }

    public async Task<List<string>> AddImages(FileUpload fileUpload, long spaceId, string subSpaceId)
    {
        var targetSpace = await _spaceGetter.GetByIdWithProperty(spaceId);
        if (targetSpace == null) throw new ArgumentNullException($"Space with id = {spaceId} not found");

        if (string.IsNullOrEmpty(subSpaceId))
        {
            var pathToImages = Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Spaces", $"{targetSpace.Id}");
            var result = await _imageUploader.ImageUpload(fileUpload.files, pathToImages);

            if (result.Any())
            {
                targetSpace.SpaceProperty.Images.AddRange(result);
                var updateSpaceModel = new UpdateSpaceModel
                {
                    SpaceId = targetSpace.Id,
                    UtilizationTypeId = targetSpace.UtilizationTypeId,
                    SpaceAddress = targetSpace.SpaceAddress,
                    Latitude = targetSpace.Latitude,
                    Longitude = targetSpace.Longitude,
                    SpaceProperty = targetSpace.SpaceProperty
                };
                _ = await _spaceService.UpdateSpace(updateSpaceModel);

                return result;
            }
        }
        else
        {
            var isValidSubspaceId = Guid.TryParse(subSpaceId, out var validSubSpaceId);
            if (!isValidSubspaceId) throw new ValidationException($"SubSpaceId = {subSpaceId} is not valid");

            //todo for subSpaces
            //var result = await _imageUploader.ImageUpload(fileUpload.files, pathToImages);
            throw new NotImplementedException("SubSpaces is not supported now");
        }

        throw new ArgumentNullException("SpaceId is necessary argument");
    }

    public async Task<(byte[], string contentType)> GetImageByName(long spaceId, string subSpaceId, string imageName)
    {
        var targetSpace = await _spaceGetter.GetById(spaceId);
        if (targetSpace == null) throw new ArgumentNullException($"Space with id = {spaceId} not found");

        string pathToImage = null;

        if (string.IsNullOrEmpty(subSpaceId))
        {
            pathToImage = Path.Combine(AlgoTectureEnvironments.GetPathToImages(), "Spaces", targetSpace.Id.ToString(), imageName);
        }
        else
        {
            //todo for subSpaces
            throw new NotImplementedException("SubSpaces is not supported now");
        }

        var mimeType = MimeTypes.GetMimeType(pathToImage);
        var result = await _imageGetter.GetImageByName(pathToImage);

        return (result, mimeType);
    }
}