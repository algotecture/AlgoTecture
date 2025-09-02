using Microsoft.AspNetCore.Mvc;

namespace Algotecture.IdentityService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", time = DateTime.UtcNow });
    }   
}