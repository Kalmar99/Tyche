namespace Tyche.StarterApp.Shared;

public class IdentityRegisteredEvent
{
    public IdentityRegisteredEvent(string id, string accountName, string userName, string email, int role)
    {
        Id = id;
        AccountName = accountName;
        Role = role;
        UserName = userName;
        Email = email;
    }

    public string Id { get; }

    public string AccountName { get; }

    public string UserName { get; }

    public string Email { get; }

    public int Role { get; }
}