using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AlgoTecture.Interfaces;
using AlgoTecture.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AlgoTecture.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IBearerAuthenticationService _bearerAuthenticationService;

        public AuthenticationController(IBearerAuthenticationService bearerAuthenticationService)
        {
            _bearerAuthenticationService = bearerAuthenticationService ?? throw new ArgumentNullException(nameof(bearerAuthenticationService));
        }

        [HttpPost("bearerAuthentication")]
        public async Task<ActionResult<BearerTokenResponseModel>> BearerAuthentication([FromBody, Required] UserCredentialModel userCredentialModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            return await _bearerAuthenticationService.BearerAuthentication(userCredentialModel);
        }
    }
}