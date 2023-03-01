namespace Tyche.StarterApp.Identity;

public class RegisterUser
{
    public string InvitationId { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string AccountId { get; set; }

    public bool IsInvalid() =>
        string.IsNullOrEmpty(InvitationId) ||
        string.IsNullOrEmpty(Name) ||
        string.IsNullOrEmpty(Email) ||
        string.IsNullOrEmpty(Password) ||
        string.IsNullOrEmpty(AccountId);
}