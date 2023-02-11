using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

internal class IdentityStorableEntityFactory
{
    private readonly HashManager _hashManager;

    public IdentityStorableEntityFactory(HashManager hashManager)
    {
        _hashManager = hashManager;
    }

    public async Task<IdentityStorableEntity> Create(string name, string email, string password, CancellationToken ct = default)
    {
        var id = Md5Hash.Generate(email);
        
        var passwordHash = await _hashManager.GeneratePasswordHash(password, id, ct);
        
        return new IdentityStorableEntity(id, email, passwordHash, name, IdentityRole.AccountAdmin);
    }
}