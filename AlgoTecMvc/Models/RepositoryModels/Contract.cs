namespace AlgoTecMvc.Models.RepositoryModels
{
    public class Contract
    {
        public long Id { get; set; }

        public long OwnerUserId { get; set; }
        
        public User OwnerUser { get; set; }

        public long TenantUserId { get; set; }
        
        public User TenantUser { get; set; }

        public long SpaceId { get; set; }

        public Space Space { get; set; }
    }
}