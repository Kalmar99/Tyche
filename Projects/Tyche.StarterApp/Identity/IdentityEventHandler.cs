using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Identity;

public class IdentityEventHandler : IEventHandler<UserInvitedEvent>
{
    private readonly IIdentityOrchestrator _orchestrator;

    public IdentityEventHandler(IIdentityOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }
    
    public async Task Handle(UserInvitedEvent @event, CancellationToken ct = default)
    {
        await _orchestrator.Invite(@event.Email, @event.AccountId, ct);
    }
}