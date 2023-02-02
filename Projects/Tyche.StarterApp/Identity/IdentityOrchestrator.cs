using System.Security.Claims;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Identity;

internal class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly IHashManager _hashManager;

    public IdentityOrchestrator(IHashManager hashManager)
    {
        _hashManager = hashManager;
    }

    public async Task<ClaimsPrincipal?> Authenticate(User user, string password, CancellationToken ct = default)
    {
        var passwordMatches = await _hashManager.VerifyPasswordHash(password, user.Password, user.Id, ct);

        if (!passwordMatches)
        {
            return null;
        }

        var claims = new List<Claim>()
        {
            new("user-role", user.Role.ToString()),
            new("user-id", user.Id)
        };

        var claimsIdentity = new ClaimsIdentity(claims);
        
        return new ClaimsPrincipal(claimsIdentity);
    }
}