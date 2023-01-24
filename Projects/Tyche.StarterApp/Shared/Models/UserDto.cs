namespace Tyche.StarterApp.Shared;

public class UserDto
{
    public UserDto(string id, string email, string password, UserRole role, string accountId)
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

public enum UserRole
{
    User = 0,
    AccountAdmin = 0,
}