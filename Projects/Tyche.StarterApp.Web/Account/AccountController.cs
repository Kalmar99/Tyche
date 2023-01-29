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
        if (dto.IsInvalid())
        {
            return BadRequest();
        }
        
        //TODO: add  authentication
        
        try
        {
            await _orchestrator.CreateAccount(dto, "test","test@example.com", "test", ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to create account");

            return StatusCode(500);
        }
    }
    
    [HttpPost("/users")]
    public async Task<IActionResult> AddUser([FromBody] UserDto dto, CancellationToken ct = default)
    {
        if (dto.IsInvalid())
        {
            return BadRequest();
        }
        
        //TODO: add  authentication
        
        try
        {
            await _orchestrator.AddUser(dto, ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to add user to account");

            return StatusCode(500);
        }
    }
    
    [HttpDelete("/users/{userId}")]
    public async Task<IActionResult> DisableUser([FromQuery] string userId, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }
        
        try
        {
            await _orchestrator.DisableUser(userId, ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to disable user with id: {userId}", userId);

            return StatusCode(500);
        }
    }
}