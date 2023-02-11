﻿using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("users")]
    public async Task<IActionResult> AddUser([FromBody] UserDto dto, CancellationToken ct = default)
    {
        if (dto.IsInvalid())
        {
            return BadRequest();
        }
        
        //TODO: add  authentication
        
        try
        {
            await _orchestrator.AttachUser(dto, ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to add user to account");

            return StatusCode(500);
        }
    }
    
    [HttpDelete("{accountId}/users/{userId}")]
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