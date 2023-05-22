using AlgoTecture.Domain.Models;

namespace AlgoTecture.Libraries.Spaces.Models.Dto;

public class AddSubSpaceModel
{
    public AddSubSpaceModel()
    {
        _subSpaces = new List<AddSubSpaceModel>();
    }

    private List<AddSubSpaceModel> _subSpaces;

    public SubSpaceDetails SubSpaceDetails { get; set; }

    public int UtilizationTypeId { get; set; }

    public string Description { get; set; }

    public List<AddSubSpaceModel> Subspaces
    {
        get { return _subSpaces; }
        set { _subSpaces = value; }
    }
}