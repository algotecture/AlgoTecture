namespace AlgoTecture.Domain.Models
{
    public class SpaceProperty
    {
        public Guid SpacePropertyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SpaceDetails SpaceDetails { get; set; }
        
        public long OwnerId { get; set; }

        public long ContractId { get; set; }

        public List<SubSpace> SubSpaces { get; set; }
    }
}   