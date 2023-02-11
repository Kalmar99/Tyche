using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Tyche.StarterApp.Shared.EmailClient;

internal class EmailClient : IEmailClient
{
    private readonly EmailSettings _settings;

    public EmailClient(EmailSettings settings)
    {
        _settings = settings;
    }
    
    public async Task Send(string recipient, string subject, string html, CancellationToken ct = default)
    {
        var message = MimeMessageFactory.CreateHtmlMessage(recipient, _settings.SenderEmail, subject, html);

        await Send(message, ct);
    }

    private async Task Send(MimeMessage message, CancellationToken ct = default)
    {
        using var client = new SmtpClient();
        
        await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.SslOnConnect, ct);
        
        await client.AuthenticateAsync(_settings.Username, _settings.Password, ct);
        
        await client.SendAsync(message, ct);
        
        await client.DisconnectAsync(false, ct);
    }
}