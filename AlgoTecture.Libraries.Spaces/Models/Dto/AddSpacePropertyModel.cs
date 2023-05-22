using AlgoTecture.Domain.Models;

namespace AlgoTecture.Libraries.Spaces.Models.Dto;

public class AddSpacePropertyModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public SpaceDetails SpaceDetails { get; set; }

    public List<AddSubSpaceModel> SubSpaces { get; set; }
}