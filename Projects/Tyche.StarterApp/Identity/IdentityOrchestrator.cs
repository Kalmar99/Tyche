using System.Security.Claims;
using Tyche.StarterApp.Identity.Storage;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Identity;

internal class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly IHashManager _hashManager;
    private readonly IdentityStorableEntityFactory _storableEntityFactory;
    private readonly IdentityRepository _repository;
    private readonly IEventDispatcher _eventDispatcher;

    public IdentityOrchestrator(IdentityStorableEntityFactory storableEntityFactory, IdentityRepository repository, IEventDispatcher eventDispatcher, IHashManager hashManager)
    {
        _hashManager = hashManager;
        _storableEntityFactory = storableEntityFactory;
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<ClaimsPrincipal?> Authenticate(string email, string password, CancellationToken ct = default)
    {
        var identity = await _repository.Get(email, ct);
        
        var passwordMatches = await _hashManager.VerifyPasswordHash(password, identity.Password, identity.Key, ct);

        if (!passwordMatches)
        {
            return null;
        }

        var claims = new List<Claim>()
        {
            new("user-role", identity.Role.ToString()),
            new("user-id", identity.Key)
        };

        var claimsIdentity = new ClaimsIdentity(claims);
        
        return new ClaimsPrincipal(claimsIdentity);
    }

    public async Task Register(RegisterDto dto, CancellationToken ct = default)
    {
        var entity = await _storableEntityFactory.Create(dto.Name, dto.Email, dto.Password, ct);

        await _repository.Set(entity, ct);
        
        _eventDispatcher.Dispatch(new IdentityRegisteredEvent(entity.Key, dto.AccountName, dto.Name,dto.Email, (int)IdentityRole.AccountAdmin));
    }
}