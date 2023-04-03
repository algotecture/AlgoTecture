namespace AlgoTecture.Domain.Models.RepositoryModels
{
    public class Space
    {
        public long Id { get; set; }

        public int UtilizationTypeId { get; set; }

        public UtilizationType UtilizationType { get; set; }

        public string SpaceAddress { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        public string SpaceProperty { get; set; }
    }
}