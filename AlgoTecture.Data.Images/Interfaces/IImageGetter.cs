namespace AlgoTecture.Data.Images.Interfaces;

public interface IImageGetter
{
    Task<byte[]> GetImageByName(string path);
}