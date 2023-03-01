using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Identity;

[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityOrchestrator _orchestrator;

    public IdentityController(IIdentityOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto, CancellationToken ct = default)
    {
        if (dto.IsInvalid())
        {
            return BadRequest();
        }

        var claimsPrincipal = await _orchestrator.Authenticate(dto.Email, dto.Password, CookieAuthenticationDefaults.AuthenticationScheme, ct);

        if (claimsPrincipal == null)
        {
            return Unauthorized();
        }

        var authSettings = AuthenticationPropertiesFactory.Create();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authSettings);
        
        return NoContent();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct = default)
    {
        if (dto.IsInvalid())
        {
            return BadRequest();
        }

        await _orchestrator.Register(dto, ct);

        return NoContent();
    }
    
    [HttpPost("register-invite")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, [FromQuery] string invitation, CancellationToken ct = default)
    {
        if (dto.IsInvalid())
        {
            return BadRequest();
        }

        var inviteWasValid = await _orchestrator.Register(invitation, dto, ct);

        if (!inviteWasValid)
        {
            return Forbid();
        }

        return NoContent();
    }
}