using System.Security.Claims;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

public interface IIdentityOrchestrator
{
    public Task<ClaimsPrincipal?> Authenticate(User user, string password, CancellationToken ct = default);
}