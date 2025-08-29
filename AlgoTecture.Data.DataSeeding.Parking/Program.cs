using System.Diagnostics;
using Algotecture.Data.DataSeeding.Parking.Models;
using Algotecture.Data.Persistence.Ef;
using Algotecture.Domain.Models;
using Algotecture.Domain.Models.RepositoryModels;
using Newtonsoft.Json;

namespace Algotecture.Data.DataSeeding.Parking
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine(@"To start press any key");
            Console.ReadKey();
            Console.WriteLine($"{typeof(Program).Namespace} has been started.");

            var sw = new Stopwatch();
            sw.Start();

            var file = await File.ReadAllTextAsync("parking.json");
            
            var deserialized = JsonConvert.DeserializeObject<Root>(file);

            if(deserialized == null) return;
            
            var newParkingSpace = new List<Space>();
            var countWithoutCoordinates = 0;
            
            foreach (var deserializedFeature in deserialized.features)
            {
                if (string.IsNullOrEmpty(deserializedFeature.geometry.coordinates[0].ToString()) || string.IsNullOrEmpty(deserializedFeature.geometry.coordinates[1].ToString()))
                {
                    countWithoutCoordinates++;
                    continue;
                }
                
                var space = new Space
                {
                    Latitude = Convert.ToDouble(deserializedFeature.geometry.coordinates[1].ToString()),
                    Longitude = Convert.ToDouble(deserializedFeature.geometry.coordinates[0].ToString()),
                    SpaceAddress = string.Empty,
                    UtilizationTypeId = 11,
                    SpaceProperty = JsonConvert.SerializeObject( new SpaceProperty
                    {
                        Name = string.Empty,
                        SpacePropertyId = Guid.NewGuid(),
                        Description = string.Empty,
                        Properties = new Dictionary<string, string>(),
                        Images = new List<string>()
                    })
                };
              newParkingSpace.Add(space);  
            }

            ApplicationDbContext context;
            await using (context = new ApplicationDbContext(Provider.NpgSql))
            {
                await context.Spaces.AddRangeAsync(newParkingSpace);
                await context.SaveChangesAsync();
            }
            
            sw.Stop();
            Console.WriteLine($"Not valid features: {countWithoutCoordinates}");
            Console.WriteLine($"Process finished success in {sw.Elapsed}");
            Console.ReadLine();
        }
    }
}