using Algotecture.Identity.Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Algotecture.IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("telegram-login")]
    public async Task<ActionResult<TelegramLoginResult>> TelegramLogin([FromBody] TelegramLoginCommand cmd)
        => Ok(await _mediator.Send(cmd));
}