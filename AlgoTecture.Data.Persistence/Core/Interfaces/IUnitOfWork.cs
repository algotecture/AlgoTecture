namespace AlgoTecture.Data.Persistence.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users {get;}
        
        ISpaceRepository Spaces {get;}
        
        IContractRepository Contracts { get; }
        
        IUserAuthenticationRepository UserAuthentications { get; }
        
        ITelegramUserInfoRepository TelegramUserInfos { get; }
        
        IUtilizationTypeRepository UtilizationTypes { get; }

        Task CompleteAsync();
    }
}