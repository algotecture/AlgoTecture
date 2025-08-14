namespace AlgoTecture.Domain.Models
{
    public class SubSpace
    {
        public SubSpace()
        {
            _subSpaces = new List<SubSpace>();
        }

        public Guid SubSpaceId { get; set; }

        private List<SubSpace>? _subSpaces;

        public Dictionary<string, string>? Properties { get; set; }

        public int UtilizationTypeId { get; set; }

        public long OwnerId { get; set; }

        public string Description { get; set; } = null!;

        public List<SubSpace>? Subspaces
        {
            get { return _subSpaces; }
            set { _subSpaces = value; }
        }
        
        public List<string> Images { get; set; } = null!;
    }
}