using AlgoTecture.Domain.Models;

namespace AlgoTecture.Libraries.Spaces.Models.Dto
{
    public class AddSpaceModel
    {
        public int UtilizationTypeId { get; set; }

        public string SpaceAddress { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        public SpaceProperty SpaceProperty { get; set; }    
    }
}