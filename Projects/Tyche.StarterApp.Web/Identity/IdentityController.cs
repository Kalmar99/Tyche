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

        var claimsPrincipal = await _orchestrator.Authenticate(dto.Email, dto.Password, ct);

        if (claimsPrincipal == null)
        {
            return Unauthorized();
        }

        var authSettings = AuthenticationPropertiesFactory.Create();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authSettings);
        
        return NoContent();
    }
}