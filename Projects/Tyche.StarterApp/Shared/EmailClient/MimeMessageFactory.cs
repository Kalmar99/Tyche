using MimeKit;

namespace Tyche.StarterApp.Shared.EmailClient;

public static class MimeMessageFactory
{
    public static MimeMessage CreateHtmlMessage(string recipient, string sender, string subject, string html)
    {
        var fromAddresses = new List<MailboxAddress>() { new(sender, sender) };
        var toAddresses = new List<MailboxAddress>() { new(recipient, recipient) };

        var body = new TextPart("html")
        {
            ContentMd5 = Md5Hash.Generate(html),
            ContentTransferEncoding = ContentEncoding.Default,
            Text = html
        };


        return new MimeMessage(fromAddresses, toAddresses, subject, body);
    }
}