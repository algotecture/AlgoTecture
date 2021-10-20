using System.Threading.Tasks;

namespace AlgoTecture.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users {get;}
        
        ISpaceRepository Spaces {get;}
        
        IContractRepository Contracts { get; }
        
        IUserAuthenticationRepository UserAuthentications { get; }

        Task CompleteAsync();
    }
}