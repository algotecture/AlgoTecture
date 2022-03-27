using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}