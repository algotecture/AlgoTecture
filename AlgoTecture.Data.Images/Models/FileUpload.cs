using Microsoft.AspNetCore.Http;

namespace Algotecture.Data.Images.Models;

public class FileUpload
{
    public IFormFileCollection? files { get; set; }
}