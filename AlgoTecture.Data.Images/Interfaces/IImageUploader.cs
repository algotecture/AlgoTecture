using Microsoft.AspNetCore.Http;

namespace Algotecture.Data.Images.Interfaces;

public interface IImageUploader
{
    Task<List<string>> ImageUpload(IFormFileCollection? formFile, string path);
}