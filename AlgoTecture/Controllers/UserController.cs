using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AlgoTecture.Domain.Models.Dto;
using AlgoTecture.Libraries.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.Controllers
{
    [Route("[controller]")]
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