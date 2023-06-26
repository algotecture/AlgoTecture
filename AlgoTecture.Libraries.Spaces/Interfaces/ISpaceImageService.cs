using AlgoTecture.Data.Images.Models;

namespace AlgoTecture.Libraries.Spaces.Interfaces;

public interface ISpaceImageService
{
    Task<List<string>> AddImages(FileUpload fileUpload, long spaceId, string subSpaceId);
    
    Task<(byte[], string contentType)> GetImageByName(long spaceId, string subSpaceId, string imageName);
}