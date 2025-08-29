﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Algotecture.Domain.Models.Dto;
using Algotecture.Libraries.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Algotecture.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
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