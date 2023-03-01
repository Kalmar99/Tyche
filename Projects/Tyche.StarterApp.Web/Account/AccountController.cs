using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tyche.StarterApp.Shared;

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

    [HttpPost("users/invite")]
    [Authorize(Policy = AuthenticationPolicies.AccountAdmin)]
    public async Task<IActionResult> InviteUser([FromBody] InviteUserRequestDto dto, CancellationToken ct = default)
    {
        try
        {
            await _orchestrator.InviteUser(dto.Email, dto.AccountId);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to invite user");

            return StatusCode(500);
        }
    }

    [HttpDelete("{accountId}/users/{userId}")]
    [Authorize(Policy = AuthenticationPolicies.AccountAdmin)]
    public async Task<IActionResult> DisableUser([FromRoute] string accountId, [FromRoute] string userId, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest();
        }
        
        try
        {
            await _orchestrator.DisableUser(userId, accountId, ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to disable user with id: {userId}", userId);

            return StatusCode(500);
        }
    }
}