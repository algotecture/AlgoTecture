using System.Threading.Tasks;

namespace AlgoTec.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users {get;}
        
        ISpaceRepository Spaces {get;}
        
        IContractRepository Contracts { get; }

        Task CompleteAsync();
    }
}