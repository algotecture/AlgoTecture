namespace AlgoTecture.Domain.Models
{
    public class SpaceProperty
    {
        public Guid SpacePropertyId { get; set; }

        public long SpaceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal CostPerMinute { get; set; }
        
        public decimal CostPerHour { get; set; }
        
        public decimal CostPerDay { get; set; }
        
        public decimal CostPerWeek { get; set; }
        
        public decimal CostPerMonth { get; set; }
        
        public decimal CostPerYear { get; set; }

        public string UnitOfPay { get; set; }

        public int UtilizationTypeId { get; set; }
        
        public double TotalArea { get; set; }
        
        public long OwnerId { get; set; }

        public long ContractId { get; set; }

        public List<SubSpace> SubSpaces { get; set; }
    }
}   