using Algotecture.Domain.Models;
using Algotecture.Libraries.Spaces.Models.Dto;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Algotecture.Libraries.Spaces.Tests.Serialization;

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
            SpaceProperty = new SpaceProperty
            {
                Name = string.Empty,
                Description = string.Empty,
                Properties = new Dictionary<string, string>(){{"BuildingName", "Name"}, {"Area", "100"}},
                
                SubSpaces = new List<SubSpace>()
                {
                    new SubSpace
                    {
                        UtilizationTypeId = 1,
                        Description = string.Empty,
                        Properties = new Dictionary<string, string>(){{"Levels", "2"}, {"Area", "45"}}
                    },
                    new SubSpace
                    {
                        UtilizationTypeId = 1,
                        Description = string.Empty,
                        Properties = new Dictionary<string, string>(){{"Levels", "2"}, {"Area", "44"}},
                        Subspaces = new List<SubSpace>(){ new SubSpace
                        {
                            UtilizationTypeId = 1,
                            Description = string.Empty,
                            Properties = new Dictionary<string, string>(){{"Levels", "1"}, {"Area", "10"}}
                        }}
                    }
                }
            }
        };

        void Code() => JsonConvert.SerializeObject(addSpaceModel);

        var serializedModel = JsonConvert.SerializeObject(addSpaceModel);
        
        Assert.That(Code, Throws.Nothing);
    }
}