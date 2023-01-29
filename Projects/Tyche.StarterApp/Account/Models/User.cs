namespace Tyche.StarterApp.Account;

public class User
{
    private const string DisabledUserName = "Disabled User";
    
    public User(string id, string email, string password, UserRole role, string accountId)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }

    public string Name { get; private set; }
    
    public string Email { get; private set; }

    public string Password { get; private set; }

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

public enum UserRole
{
    User = 0,
    AccountAdmin = 0,
    Disabled,
}