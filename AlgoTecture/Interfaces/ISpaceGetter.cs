using System.Threading.Tasks;
using AlgoTecture.Models.RepositoryModels;

namespace AlgoTecture.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}