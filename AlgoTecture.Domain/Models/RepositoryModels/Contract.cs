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
        
        public decimal Cost { get; set; }
        
        public int? UtilizationTypeId { get; set; }
        
        public UtilizationType UtilizationType { get; set; }

        public DateTime? DeclarationDateTime { get; set; }
        
        public DateTime? ContractFrom { get; set; }

        public DateTime? ContractTo { get; set; }

        public DateTime? ContractDateTime { get; set; }
        
        public DateTime? BookingDateTime { get; set; }

        public DateTime? BookedFrom { get; set; }
        
        public DateTime? BookedTo { get; set; }
        
        public DateTime? CancellationBookedDateTime { get; set; }
    }
}