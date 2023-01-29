namespace Tyche.StarterApp.Account;

public class UserDto
{
    public UserDto(string id, string name, string email, string password, UserRole role, string accountId)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        AccountId = accountId;
    }

    public string Id { get; }

    public string Name { get; }
    
    public string Email { get; }

    public string Password { get; }

    public UserRole Role { get; }

    public string AccountId { get; }

    public bool IsInvalid() =>
        string.IsNullOrEmpty(Name) ||
        string.IsNullOrEmpty(Email) ||
        string.IsNullOrEmpty(Password) ||
        string.IsNullOrEmpty(AccountId);
}

public enum UserRole
{
    User = 0,
    AccountAdmin = 0,
    Disabled,
}