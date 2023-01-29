namespace Tyche.StarterApp.Account;

public interface IAccountOrchestrator
{
    public Task<AccountDto> GetAccount(string accountId, CancellationToken ct = default);

    public Task CreateAccount(AccountDto dto, string userEmail, string userPassword, CancellationToken ct = default);

    public Task AddUser(User user, CancellationToken ct = default);

    public Task DisableUser(User user, CancellationToken ct = default);
}