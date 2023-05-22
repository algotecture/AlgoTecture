using System;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Libraries.Spaces.Interfaces;
using AlgoTecture.Libraries.Spaces.Models.Dto;
using AlgoTecture.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AddSubSpaceModel = AlgoTecture.Domain.Models.Dto.AddSubSpaceModel;

namespace AlgoTecture.WebApi.Controllers
{
    [Route("[controller]")]
    public class SpaceController : Controller
    {
        private readonly ISpaceGetter _spaceGetter;
        private readonly ISubSpaceService _subSpaceService;
        private readonly ISpaceService _spaceService;

        public SpaceController(ISpaceGetter spaceGetter, ISubSpaceService subSpaceService, ISpaceService spaceService)
        {
            _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
            _subSpaceService = subSpaceService ?? throw new ArgumentNullException(nameof(subSpaceService));
            _spaceService = spaceService;
        }
        
        [HttpGet("GetByCoordinates")]
        public async Task<ActionResult<Space>> GetSpaceByCoordinates([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _spaceGetter.GetByCoordinates(latitude, longitude);
        }
        
        [Authorize]
        [HttpPost("AddSubSpaceToSpace")]
        public async Task<ActionResult<Space>> AddSubSpace([FromBody] AddSubSpaceModel addSubSpaceModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _subSpaceService.AddSubSpaceToSpace(addSubSpaceModel);
        }
        
        [HttpPost("AddSpace")]
        public async Task<ActionResult<Space>> AddSpace([FromBody] AddSpaceModel addSpaceModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            return await _spaceService.AddSpace(addSpaceModel);
        }
    }
}