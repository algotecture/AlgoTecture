namespace AlgoTecture.Domain.Models
{
    public class SpaceProperty
    {
        public Guid SpacePropertyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public List<SubSpace> SubSpaces { get; set; }

        public List<string> Images { get; set; }
    }
}   