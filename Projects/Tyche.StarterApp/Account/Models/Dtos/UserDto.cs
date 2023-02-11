namespace Tyche.StarterApp.Account;

public class UserDto
{
    public UserDto(string id, string name, string email, UserRole role, string accountId)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }

    public string Name { get; }
    
    public string Email { get; }

    public UserRole Role { get; }

    public string AccountId { get; }

    public bool IsInvalid() =>
        string.IsNullOrEmpty(Name) ||
        string.IsNullOrEmpty(Email) ||
        string.IsNullOrEmpty(AccountId);
}

public enum UserRole
{
    User = 0,
    AccountAdmin = 1,
    Disabled = 2,
}