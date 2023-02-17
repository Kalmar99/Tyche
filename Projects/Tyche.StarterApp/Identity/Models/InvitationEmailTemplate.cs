namespace Tyche.StarterApp.Identity;

public class InvitationEmailTemplate
{
    public InvitationEmailTemplate(string html)
    {
        Html = html;
    }
    
    private const string TemplateFilePath = "Identity/Static/InvitationTemplate.html";

    public string Html { get; }

    public string Subject => "You have been invited to join Tyche!";
    
    public static async Task<InvitationEmailTemplate> Create(string inviteUrl, string invitationKey, CancellationToken ct = default)
    {
        var html = await File.ReadAllTextAsync(TemplateFilePath, ct);

        var htmlWithValues = html
            .Replace("{INVITE_KEY}", invitationKey)
            .Replace("{INVITE_URL}", inviteUrl);

        return new InvitationEmailTemplate(htmlWithValues);
    }
}