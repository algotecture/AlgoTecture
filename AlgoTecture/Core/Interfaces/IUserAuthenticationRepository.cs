using System.Threading.Tasks;

namespace AlgoTecture.Core.Interfaces
{
    public interface IUserAuthenticationRepository
    {
        Task<bool> IsValidPasswordAsync(long userId, string hashedPassword);
    }
}