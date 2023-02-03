using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.CommonUserRepository;

internal class CommonUserEntity : StorageEntity
{
    public CommonUserEntity(string key, string name, string email, string password, UserRole role, string accountId) 
        : base(key, email)
    {
        Key = key;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Name { get; }
    
    public string Email { get; }

    public string Password { get; }

    public UserRole Role { get; }

    public string AccountId { get; }
}