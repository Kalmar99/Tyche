using System.Security.Claims;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

public interface IIdentityOrchestrator
{
    public Task<ClaimsPrincipal?> Authenticate(string email, string password, CancellationToken ct = default);

    public Task Register(RegisterDto dto, CancellationToken ct = default);
}