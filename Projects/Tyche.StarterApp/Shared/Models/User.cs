using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Shared;

public class User
{
    public User(string id, string name, string email, string password, UserRole role, string accountId)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }
    
    public string Name { get; private set; }
    
    public string Email { get; private set; }

    public string Password { get; private set; }

    public UserRole Role { get; private set; }

    public string AccountId { get; }

    public void Disable()
    {
        Name = "Disabled User";
        Email = string.Empty;
        Password = string.Empty;
        Role = UserRole.Disabled;
    }
}