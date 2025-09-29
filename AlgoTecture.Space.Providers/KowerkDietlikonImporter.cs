using NetTopologySuite.Geometries;

namespace AlgoTecture.Space.Providers;

public class KowerkDietlikonImporter : ISpaceProviderImporter
{
   public string ProviderName => "KowerkDietlikon";

   public Task<IEnumerable<Domain.Space>> ImportAsync()
   {
      var buildingId = Guid.NewGuid();

      var building = new Domain.Space
      {
         Id = buildingId,
         Name = "Parking Building Industriestrasse 24",
         SpaceTypeId = 1, 
         SpaceAddress = "Industriestrasse 24, 8305 Dietlikon",
         Location = new Point(47.413565, 8.620584),
         Description = "Main building with underground and street parking",
         DataSource = ProviderName,
         ExternalId = null
      };

      var spaces = new List<Domain.Space> { building };

      // Underground
      for (int i = 1; i <= 63; i++)
      {
         spaces.Add(new Domain.Space
         {
            Id = Guid.NewGuid(),
            ParentId = building.Id,
            SpaceTypeId = 1,
            Name = $"P{i:D2}",
            Description = $"Underground parking space {i:D2}",
            SpaceAddress = building.SpaceAddress,
            Location = building.Location,
            DataSource = ProviderName,
            ExternalId = null,
            SpaceProperties = """{ "Level": -1, "Covered": true, "Type": "Underground" }"""
         });
      }

      // Street
      for (int i = 1; i <= 2; i++)
      {
         spaces.Add(new Domain.Space
         {
            Id = Guid.NewGuid(),
            ParentId = building.Id,
            SpaceTypeId = 1,
            Name = $"S{i:D2}",
            Description = $"Street parking space {i:D2}",
            SpaceAddress = building.SpaceAddress,
            Location = building.Location,
            DataSource = ProviderName,
            ExternalId = null,
            SpaceProperties = """{ "Type": "Street", "Covered": false }"""
         });
      }

      return Task.FromResult<IEnumerable<Domain.Space>>(spaces);
   }
}