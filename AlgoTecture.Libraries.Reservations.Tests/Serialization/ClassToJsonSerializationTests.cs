using AlgoTecture.Domain.Models;
using AlgoTecture.Libraries.Spaces.Models.Dto;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AlgoTecture.Libraries.Reservations.Tests.Serialization;

public class ClassToJsonSerializationTests
{
    [Test]
    public void ConversationClassToJsonSimpleTest()
    {
        var addSpaceModel = new AddSpaceModel
        {
            UtilizationTypeId = 1,
            SpaceAddress = "Unterstaldig 1 6106 Werthenstein",
            Latitude = 47.04173191647986,
            Longitude = 8.097301555686508,
            SpaceProperty = new AddSpacePropertyModel
            {
                Name = string.Empty,
                Description = string.Empty,
                SpaceDetails = new SpaceDetails
                {
                   BuildingYear = "2023",
                   BuildingName = string.Empty,
                   Levels = "3",
                   Area = "456.4",
                   Flats = "1",
                   FloorArea = "674",
                   BuildingClass = "Gebäude mit 1 Wohnung",
                   BuildingCategory = "Andere Wohngebäude",
                   PlaceName = "Werthenstein",
                   MunicipalityId = "1009",
                   MunicipalityName = "Werthenstein"
                },
                SubSpaces = new List<AddSubSpaceModel>()
                {
                    new AddSubSpaceModel
                    {
                        UtilizationTypeId = 1,
                        Description = string.Empty,
                        SubSpaceDetails = new SubSpaceDetails
                        {
                            Levels = "1",
                            Area = "300",
                            Flats = "1",
                            FloorArea = "477",
                        }
                    },
                    new AddSubSpaceModel
                    {
                        UtilizationTypeId = 1,
                        Description = string.Empty,
                        SubSpaceDetails = new SubSpaceDetails
                        {
                            Levels = "2",
                            Area = "200",
                            Flats = "1",
                            FloorArea = "287",
                        }
                    }
                }
            }
        };

        void Code() => JsonConvert.SerializeObject(addSpaceModel);

        var serializedModel = JsonConvert.SerializeObject(addSpaceModel);
        
        Assert.That(Code, Throws.Nothing);
    }
}