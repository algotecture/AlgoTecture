using System.Threading.Tasks;
using AlgoTecMvc.Models;
using AlgoTecMvc.Models.Dto;

namespace AlgoTecMvc.Interfaces
{
    public interface ISubSpaceService
    {
        Task<SubSpace> AddSubSpace(AddSubSpaceModel addSubSpaceModel);
    }
}