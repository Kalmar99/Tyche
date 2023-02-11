using System.Security.Claims;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Identity;

internal class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly HashManager _hashManager;
    private readonly IdentityStorableEntityFactory _storableEntityFactory;
    private readonly IdentityRepository _repository;
    private readonly IEventDispatcher _eventDispatcher;

    public IdentityOrchestrator(IdentityStorableEntityFactory storableEntityFactory, IdentityRepository repository, IEventDispatcher eventDispatcher, HashManager hashManager)
    {
        _hashManager = hashManager;
        _storableEntityFactory = storableEntityFactory;
        _repository = repository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<ClaimsPrincipal?> Authenticate(string email, string password, string scheme, CancellationToken ct = default)
    {
        var identity = await _repository.Get(email, ct);
        
        var passwordMatches = await _hashManager.VerifyPasswordHash(password, identity.Password, identity.Key, ct);

        if (!passwordMatches)
        {
            return null;
        }

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Role, identity.Role.ToString()),
            new(ClaimTypes.Sid, identity.Key)
        };

        var claimsIdentity = new ClaimsIdentity(claims, scheme);
        
        return new ClaimsPrincipal(claimsIdentity);
    }

    public async Task Register(RegisterDto dto, CancellationToken ct = default)
    {
        var entity = await _storableEntityFactory.Create(dto.Name, dto.Email, dto.Password, ct);

        await _repository.Set(entity, ct);
        
        _eventDispatcher.Dispatch(new IdentityRegisteredEvent(entity.Key, dto.AccountName, dto.Name,dto.Email, (int)IdentityRole.AccountAdmin));
    }
}