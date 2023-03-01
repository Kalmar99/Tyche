namespace Tyche.StarterApp.Shared;

public class UserInvitedEvent
{
    public UserInvitedEvent(string accountId, string email)
    {
        AccountId = accountId;
        Email = email;
    }
    
    public string AccountId { get; }

    public string Email { get; }
}