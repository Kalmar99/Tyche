using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Account;

public class UserFactory
{
    private readonly IHashManager _hashManager;

    public UserFactory(IHashManager hashManager)
    {
        _hashManager = hashManager;
    }
    
    public async Task<User> Create(string name, string email, string password, UserRole role, string accountId, CancellationToken ct = default)
    {
        var userId = Md5Hash.Generate(email);

        var hashedPassword = await _hashManager.GeneratePasswordHash(password, userId, ct);

        return new User(userId, name, email, hashedPassword, role, accountId);
    }
}