using Algotecture.Data.Images.Interfaces;

namespace Algotecture.Data.Images.Implementations;

public class ImageService : IImageService
{
   public async Task<byte[]> GetImageByName(string path)
   {
      if (File.Exists(path))
      {
         var bytes = await File.ReadAllBytesAsync(path);

         return bytes;
      }

      throw new ArgumentNullException("Сould not find the path to the required image");
   }

   public List<string> GetImageNamesBySpaceId(string path)
   {
      if (Directory.Exists(path))
      {
         DirectoryInfo directory = new DirectoryInfo(path);

         var imageNames = new List<string>();
         
         foreach (var image in directory.GetFiles())
         {
            imageNames.Add((image.Name));
         }

         return imageNames;

      }
      throw new ArgumentNullException("Сould not find the path to the required image");
   }
   
   public bool RemoveImage(string path)
   {
      if (File.Exists(path))
      {
       File.Delete(path);
       return true;
      }
      throw new ArgumentNullException("Сould not find the path to the required image");
   }
}