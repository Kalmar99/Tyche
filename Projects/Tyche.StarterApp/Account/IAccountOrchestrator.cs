namespace Tyche.StarterApp.Account;

public interface IAccountOrchestrator
{
    Task AttachUser(UserDto userDto, CancellationToken ct = default);

    public Task DisableUser(string userId, string accountId, CancellationToken ct = default);
}