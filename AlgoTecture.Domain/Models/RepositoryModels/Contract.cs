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
        
        public string PriceCurrency { get; set; }

        public string TotalPrice { get; set; }
        
        public long PriceSpecificationId { get; set; }

        public PriceSpecification PriceSpecification { get; set; }
        
        public int? UtilizationTypeId { get; set; }
        
        public UtilizationType UtilizationType { get; set; }
        
        public long? ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public DateTime? DeclarationDateTime { get; set; }
        
        public DateTime? ContractFrom { get; set; }

        public DateTime? ContractTo { get; set; }

        public DateTime? ContractDateTime { get; set; }
    }
}