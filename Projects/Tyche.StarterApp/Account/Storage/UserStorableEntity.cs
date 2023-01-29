using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class UserStorableEntity : StorageEntity
{
    public UserStorableEntity(string id, string email, string password, UserRole role, string accountId) 
        : base(id, email)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }
    
    public string Email { get; }

    public string Password { get; }

    public UserRole Role { get; }

    public string AccountId { get; }
}