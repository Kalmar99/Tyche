namespace Tyche.StarterApp.Identity;

public class IdentitySettings
{
    public string InvitationUrl { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(InvitationUrl))
        {
            throw new ArgumentNullException($"{nameof(IdentitySettings)}:{nameof(IdentitySettings.InvitationUrl)} cannot be null or empty");
        }
    }
}