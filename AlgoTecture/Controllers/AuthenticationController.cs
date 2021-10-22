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
        private readonly IBearerAuthenticator _bearerAuthenticator;

        public AuthenticationController(IBearerAuthenticator bearerAuthenticator)
        {
            _bearerAuthenticator = bearerAuthenticator ?? throw new ArgumentNullException(nameof(bearerAuthenticator));
        }

        [HttpPost("bearerAuthentication")]
        public async Task<ActionResult<BearerTokenResponseModel>> BearerAuthentication([FromBody, Required] UserCredentialModel userCredentialModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            return await _bearerAuthenticator.BearerAuthentication(userCredentialModel);
        }
    }
}