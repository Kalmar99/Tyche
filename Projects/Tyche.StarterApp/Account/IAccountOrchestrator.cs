namespace Tyche.StarterApp.Account;

public interface IAccountOrchestrator
{
    public Task<Account> Get(string accountId, CancellationToken ct = default);

    Task AttachUser(UserDto userDto, CancellationToken ct = default);

    public Task DisableUser(string userId, string accountId, CancellationToken ct = default);
}