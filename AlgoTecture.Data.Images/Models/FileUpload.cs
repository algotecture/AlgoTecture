using Microsoft.AspNetCore.Http;

namespace AlgoTecture.Data.Images.Models;

public class FileUpload
{
    public IFormFileCollection? files { get; set; }
}