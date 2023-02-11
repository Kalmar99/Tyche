using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class UserStorableEntity : StorageEntity
{
    public UserStorableEntity(string key, string name, string email, UserRole role, string accountId) 
        : base(key, email)
    {
        Key = key;
        Name = name;
        Email = email;
        Role = role;
        AccountId = accountId;
    }

    public string Name { get; }
    
    public string Email { get; }

    public UserRole Role { get; }

    public string AccountId { get; }
}