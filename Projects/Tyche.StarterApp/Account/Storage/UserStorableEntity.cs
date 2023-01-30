using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

internal class UserStorableEntity : StorageEntity
{
    private const string DisabledUserName = "Disabled User";
    
    public UserStorableEntity(string key, string name, string email, string password, UserRole role, string accountId) 
        : base(key, email)
    {
        Key = key;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Name { get; private set; }
    
    public string Email { get; private set; }

    public string Password { get; private set;}

    public UserRole Role { get; private set;}

    public string AccountId { get; }

    public void Disable()
    {
        Email = string.Empty;
        Password = string.Empty;
        Role = UserRole.Disabled;
        Name = DisabledUserName;
    }
}