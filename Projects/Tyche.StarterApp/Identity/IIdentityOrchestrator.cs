using System.Security.Claims;

namespace Tyche.StarterApp.Identity;

public interface IIdentityOrchestrator
{
    public Task<ClaimsPrincipal?> Authenticate(string email, string password, string scheme, CancellationToken ct = default);

    public Task Register(RegisterDto dto, CancellationToken ct = default);

    public Task Invite(string email, string accountId, CancellationToken ct = default);

    public Task<bool> Register(string token, RegisterDto dto, CancellationToken ct = default);
}