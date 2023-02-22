using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Account;

internal class AccountEventHandler : IEventHandler<IdentityRegisteredEvent>, IEventHandler<UserRegisteredEvent>
{
    private readonly AccountService _accountService;

    public AccountEventHandler(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task Handle(IdentityRegisteredEvent @event, CancellationToken ct = default)
    {
        var account = AccountFactory.Create(@event.AccountName, @event.UserName, @event.Email);

        await _accountService.Update(account, ct);
    }

    public async Task Handle(UserRegisteredEvent @event, CancellationToken ct = default)
    {
        var account = await _accountService.Get(@event.AccountId, ct);

        var user = UserFactory.Create(@event.Name, @event.Email, UserRole.User, @event.AccountId);
        
        account.AddUser(user);
    }
}