namespace Tyche.StarterApp.Shared;

public class UserRegisteredEvent
{
    public UserRegisteredEvent(string email, string name, string accountId)
    {
        Email = email;
        Name = name;
        AccountId = accountId;
    }

    public string Email { get; }
    
    public string Name { get; }

    public string AccountId { get; }
}