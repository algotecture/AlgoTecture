using AlgoTecture.Models.Entities;

namespace AlgoTecture.Models.RepositoryModels
{
    public class Space
    {
        public long Id { get; set; }

        public int TypeOfSpaceId { get; set; }

        public TypeOfSpace TypeOfSpace { get; set; }

        public string SpaceAddress { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public string SpaceProperty { get; set; }
    }
}