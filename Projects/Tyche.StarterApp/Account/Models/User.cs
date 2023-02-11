namespace Tyche.StarterApp.Account;

internal class User
{
    public User(string id, string name, string email, UserRole role, string accountId)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }
    
    public string Name { get; private set; }
    
    public string Email { get; private set; }

    public UserRole Role { get; private set; }

    public string AccountId { get; }

    public void Disable()
    {
        Name = "Disabled User";
        Email = string.Empty;
        Role = UserRole.Disabled;
    }
}