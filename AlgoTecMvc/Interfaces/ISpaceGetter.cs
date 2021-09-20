using System.Threading.Tasks;
using AlgoTecMvc.Models.RepositoryModels;

namespace AlgoTecMvc.Interfaces
{
    public interface ISpaceGetter
    {
        Task<Space> GetByCoordinates(double latitude, double longitude);
    }
}