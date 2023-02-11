using Tyche.StarterApp.Identity.Storage;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Identity;

public class IdentityStorableEntityFactory
{
    private readonly IHashManager _hashManager;

    public IdentityStorableEntityFactory(IHashManager hashManager)
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