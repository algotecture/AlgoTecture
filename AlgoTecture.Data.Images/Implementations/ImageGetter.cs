using AlgoTecture.Data.Images.Interfaces;

namespace AlgoTecture.Data.Images.Implementations;

public class ImageGetter : IImageGetter
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
}