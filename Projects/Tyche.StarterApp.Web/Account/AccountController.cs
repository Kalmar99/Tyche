using Microsoft.AspNetCore.Mvc;

namespace Tyche.StarterApp.Account;

[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AccountDto dto, CancellationToken ct = default)
    {
        try
        {
            await _accountRepository.Set(dto, ct);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to create account");

            return StatusCode(500);
        }
    }
}