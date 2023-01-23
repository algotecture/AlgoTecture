using System;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Domain.Models.RepositoryModels;
using AlgoTecture.Interfaces;
using AlgoTecture.Libraries.Space.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.Controllers
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
    }
}