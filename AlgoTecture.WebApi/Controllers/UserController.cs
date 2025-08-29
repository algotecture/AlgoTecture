using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Algotecture.Domain.Models.Dto;
using Algotecture.Libraries.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Algotecture.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("create")]
        public async Task<ActionResult<BearerTokenResponseModel>> Create([FromBody, Required] UserCredentialModel userCredentialModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            return await _userService.Create(userCredentialModel);
        } 
    }
}