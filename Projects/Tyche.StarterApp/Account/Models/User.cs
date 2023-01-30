using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

public class User
{
    public User(string name, string email, string password, UserRole role, string accountId)
    {
        Id = Md5Hash.Generate(email);
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