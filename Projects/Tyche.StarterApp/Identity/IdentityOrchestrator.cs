using System.Security.Claims;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.CommonUserRepository;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Identity;

internal class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly IHashManager _hashManager;
    private readonly ICommonUserRepository _commonUserRepository;

    public IdentityOrchestrator(IHashManager hashManager, ICommonUserRepository commonUserRepository)
    {
        _hashManager = hashManager;
        _commonUserRepository = commonUserRepository;
    }

    public async Task<ClaimsPrincipal?> Authenticate(string email, string password, CancellationToken ct = default)
    {
        var user = await _commonUserRepository.GetByEmail(email, ct);
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