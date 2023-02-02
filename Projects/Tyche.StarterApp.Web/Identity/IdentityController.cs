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
    public async Task<IActionResult> Login()
    {
        //TODO: get user and pass it to identity
        return Ok();
    }
}