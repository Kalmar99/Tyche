using Tyche.StarterApp.Identity.Models;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

public interface IIdentityOrchestrator
{
    public Task<AuthenticationResultDto> Authenticate(User user, CancellationToken ct = default);
}