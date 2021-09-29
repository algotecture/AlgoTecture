using System;
using System.Threading.Tasks;
using AlgoTec.Interfaces;
using AlgoTec.Models.Dto;
using AlgoTec.Models.RepositoryModels;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTec.Controllers
{
    [Route("[controller]")]
    public class SpaceController : Controller
    {
        private readonly ISpaceGetter _spaceGetter;
        private readonly ISubSpaceService _subSpaceService;

        public SpaceController(ISpaceGetter spaceGetter, ISubSpaceService subSpaceService)
        {
            _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
            _subSpaceService = subSpaceService ?? throw new ArgumentNullException(nameof(subSpaceService));
        }

        [HttpGet("GetByCoordinates")]
        public async Task<Space> GetSpaceByCoordinates([FromQuery] double latitude, [FromQuery] double longitude)
        {
            return await _spaceGetter.GetByCoordinates(latitude, longitude);
        }

        [HttpPost("AddSubSpaceToSpace")]
        public async Task<ActionResult<Space>> AddSubSpace([FromBody] AddSubSpaceModel addSubSpaceModel)
        {
            return await _subSpaceService.AddSubSpaceToSpace(addSubSpaceModel);
        }
    }
}