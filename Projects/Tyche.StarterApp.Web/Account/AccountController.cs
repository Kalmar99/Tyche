using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Account;

[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountOrchestrator _orchestrator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountOrchestrator orchestrator, ILogger<AccountController> logger)
    {
        _orchestrator = orchestrator;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AccountDto dto, CancellationToken ct = default)
    {
        try
        {
            await _orchestrator.CreateAccount(dto, "test@example.com", "test", ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to create account");

            return StatusCode(500);
        }
    }
}