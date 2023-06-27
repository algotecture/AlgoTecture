using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlgoTecture.Data.Images.Models;
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
        private readonly ISpaceImageService _spaceImageService;

        public SpaceController(ISpaceGetter spaceGetter, ISubSpaceService subSpaceService, ISpaceService spaceService, ISpaceImageService spaceImageService)
        {
            _spaceGetter = spaceGetter ?? throw new ArgumentNullException(nameof(spaceGetter));
            _subSpaceService = subSpaceService ?? throw new ArgumentNullException(nameof(subSpaceService));
            _spaceService = spaceService;
            _spaceImageService = spaceImageService;
        }
        
        [HttpGet("GetByCoordinates")]
        public async Task<ActionResult<Space>> GetSpaceByCoordinates([FromQuery] double latitude, [FromQuery] double longitude)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _spaceGetter.GetByCoordinates(latitude, longitude);
        }
        
        [HttpGet("GetById")]
        public async Task<ActionResult<Space>> GetSpaceById([FromQuery] long spaceId)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            return await _spaceGetter.GetById(spaceId);
        }
        
        [Authorize]
        [HttpPost("AddSubSpaceToSpace")]
        [ApiExplorerSettings(IgnoreApi = true)]
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
        
        [HttpPost("AddImages")]
        public async Task<ActionResult<List<string>>> AddImages([FromForm] FileUpload fileUpload, [FromQuery] long spaceId, string subSpaceId)
        {
            if (!ModelState.IsValid) return BadRequest();

            return await _spaceImageService.AddImages(fileUpload, spaceId, subSpaceId);
        }
        
        [HttpGet("GetImageByName")]
        public async Task<IActionResult> GetImageByName([FromQuery] long spaceId, [FromQuery] string subSpaceId, [FromQuery] string imageName)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _spaceImageService.GetImageByName(spaceId, subSpaceId, imageName);

            return File(result.Item1, result.Item2);
        }
        
        [HttpGet("GetImageNamesBySpaceId")]
        public async Task<ActionResult<List<string>>> GetImageNamesBySpaceId([FromQuery] long spaceId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _spaceImageService.GetImageNamesBySpaceId(spaceId);

            return result;
        }
        
        [HttpGet("RemoveImage")]
        public async Task<ActionResult> RemoveImage([FromQuery] long spaceId, [FromQuery] string imageName)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _spaceImageService.RemoveImage(spaceId, imageName);

            return Ok();
        }
    }
}