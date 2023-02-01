namespace AlgoTecture.Domain.Models
{
    public class SpaceProperty
    {
        public Guid SpacePropertyId { get; set; }

        public long SpaceId { get; set; }

        public string Name { get; set; }
        
        public int TypeOfSpace { get; set; }
        
        public double TotalArea { get; set; }
        
        public long OwnerId { get; set; }

        public long ContractId { get; set; }

        public List<SubSpace> SubSpaces { get; set; }
    }
}   