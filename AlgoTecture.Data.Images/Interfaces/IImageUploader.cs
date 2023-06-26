using Microsoft.AspNetCore.Http;

namespace AlgoTecture.Data.Images.Interfaces;

public interface IImageUploader
{
    Task<List<string>> ImageUpload(IFormFileCollection formFile, string path);
}