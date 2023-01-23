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

        public List<SubSpace> Subspaces
        {
            get { return _subSpaces; }
            set { _subSpaces = value; }
        }
    }
}