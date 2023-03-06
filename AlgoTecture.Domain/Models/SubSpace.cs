namespace AlgoTecture.Domain.Models
{
    public class SubSpace
    {
        public SubSpace()
        {
            _subSpaces = new List<SubSpace>();
        }

        public Guid SubSpaceId { get; set; }

        public int SubSpaceIdHash { get; set; }

        public long SpaceId { get; set; }

        private List<SubSpace> _subSpaces;
        
        public double Area { get; set; }

        public int UtilizationTypeId { get; set; }

        public long OwnerId { get; set; }

        public string Description { get; set; }
        
        public decimal CostPerMinute { get; set; }
        
        public decimal CostPerHour { get; set; }
        
        public decimal CostPerDay { get; set; }
        
        public decimal CostPerWeek { get; set; }
        
        public decimal CostPerMonth { get; set; }
        
        public decimal CostPerYear { get; set; }
        
        public string UnitOfPay { get; set; }

        public List<SubSpace> Subspaces
        {
            get { return _subSpaces; }
            set { _subSpaces = value; }
        }
    }
}