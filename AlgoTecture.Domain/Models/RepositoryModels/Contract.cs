using AlgoTecture.Domain.Models.Entities;

namespace AlgoTecture.Domain.Models.RepositoryModels
{
    public class Contract
    {
        public Guid Id { get; set; }

        public long OwnerUserId { get; set; }
        
        public User OwnerUser { get; set; }

        public long? TenantUserId { get; set; }
        
        public User TenantUser { get; set; }

        public long SpaceId { get; set; }

        public Space Space { get; set; }

        public Guid SubSpaceId { get; set; }

        public DateTime ContractDateStart { get; set; }

        public DateTime ContractDateStop { get; set; }

        public decimal Cost { get; set; }
        
        public int? UtilizationTypeId { get; set; }

        public UtilizationType UtilizationType { get; set; }

        public DateTime DeclarationDateTime { get; set; }

        public DateTime? ContractDateTime { get; set; }
    }
}