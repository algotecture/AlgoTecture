namespace Algotecture.Data.Persistence.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users {get;}
        
        ISpaceRepository Spaces {get;}

        IUserAuthenticationRepository UserAuthentications { get; }
        
        ITelegramUserInfoRepository TelegramUserInfos { get; }
        
        IUtilizationTypeRepository UtilizationTypes { get; }
        
        IReservationRepository Reservations { get; }
        
        IPriceSpecificationRepository PriceSpecifications { get; }

        Task CompleteAsync();
    }
}