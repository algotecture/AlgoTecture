using AlgoTecMvc.Models.Entities;

namespace AlgoTecMvc.Models.RepositoryModels
{
    public class Space
    {
        public long Id { get; set; }

        public int TypeOfSpaceId { get; set; }

        public TypeOfSpace TypeOfSpace { get; set; }
        
        public string SpaceProperty { get; set; }
    }
}