namespace Tyche.StarterApp.Account;

public interface IAccountRepository
{
    public Task Set(AccountDto dto, CancellationToken ct = default);

    public Task<AccountDto> Get(string key, CancellationToken ct = default);
}