using Microsoft.Extensions.Logging;

namespace Tyche.StarterApp.Account;

internal class AccountService
{
    private readonly UserRepository _userRepository;
    private readonly AccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;

    public AccountService(
        UserRepository userRepository,
        AccountRepository accountRepository,
        ILogger<AccountService> logger)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }
    
    public async Task<Account> Get(string accountId, CancellationToken ct = default)
    {
        try
        {
            var account = await _accountRepository.Get(accountId, ct);
            var users = await _userRepository.Get(account.Users, ct);

            return AccountMapper.Map(account, users);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Failed to get {nameof(Account)} with id: {accountId}");
            throw;
        }
    }

    public async Task Update(Account account, CancellationToken ct = default)
    {
        try
        {
            var accountStorableEntity = AccountMapper.Map(account);
            var userStorableEntities = account.Users.Select(UserMapper.Map).ToList();

            await _accountRepository.Set(accountStorableEntity, ct);
            await _userRepository.Set(userStorableEntities, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Failed to update {nameof(Account)} with id: {account.Id}");
            throw;
        }
    }
}