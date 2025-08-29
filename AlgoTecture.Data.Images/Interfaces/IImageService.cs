namespace Algotecture.Data.Images.Interfaces;

public interface IImageService
{
    Task<byte[]> GetImageByName(string path);
    
    List<string> GetImageNamesBySpaceId(string path);

    bool RemoveImage(string path);
}