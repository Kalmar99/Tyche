using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Account;

internal class AccountEventHandler : IEventHandler<IdentityRegisteredEvent>
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
}