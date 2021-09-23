using System.Threading.Tasks;
using AlgoTec.Models.RepositoryModels;

namespace AlgoTec.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}