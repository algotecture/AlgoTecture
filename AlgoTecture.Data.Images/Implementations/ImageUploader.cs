using System.ComponentModel.DataAnnotations;
using AlgoTecture.Data.Images.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AlgoTecture.Data.Images.Implementations;

public class ImageUploader : IImageUploader
{
    public async Task<List<string>> ImageUpload(IFormFileCollection formFiles, string path)
    {
        if (formFiles == null) throw new ArgumentNullException(nameof(formFiles));
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));
        if (formFiles.Count == 0) throw new ValidationException("There is no image in the form");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var imageIds = new List<string>();
        foreach (var formFile in formFiles)
        {
            var idForFile = Guid.NewGuid();

            if (formFile.FileName.Length > 50) throw new ValidationException("File name must not exceed 50 characters");
            
            var fileName = $"{idForFile.ToString()}_{formFile.FileName}";
            var targetPath = Path.Combine(path, fileName);
            await using var fileStream = File.Create(targetPath);
            await formFile.CopyToAsync(fileStream);
            
            imageIds.Add(fileName);
        }

        return imageIds;
    }
}