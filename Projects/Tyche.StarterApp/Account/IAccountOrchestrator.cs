namespace Tyche.StarterApp.Account;

public interface IAccountOrchestrator
{
    public Task<AccountDto> GetAccount(string accountId, CancellationToken ct = default);

    public Task<string> CreateAccount(AccountDto dto, UserDto userDto, CancellationToken ct = default);

    public Task AddUser(UserDto userDto, CancellationToken ct = default);

    public Task DisableUser(string userId, CancellationToken ct = default);
}