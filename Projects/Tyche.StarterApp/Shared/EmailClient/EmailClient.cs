using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Tyche.StarterApp.Shared.EmailClient;

internal class EmailClient : IEmailClient
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailClient> _logger;

    public EmailClient(EmailSettings settings, ILogger<EmailClient> logger)
    {
        _settings = settings;
        _logger = logger;
    }
    
    public async Task Send(string recipient, string subject, string html, CancellationToken ct = default)
    {
        var message = MimeMessageFactory.CreateHtmlMessage(recipient, _settings.SenderEmail, subject, html);

        await Send(message, ct);
    }

    private async Task Send(MimeMessage message, CancellationToken ct = default)
    {
        try
        {
            using var client = new SmtpClient();
        
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.Auto, cancellationToken: ct);

            if (!_settings.IsDevelopment)
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, ct);
            }

            await client.SendAsync(message, ct);
        
            await client.DisconnectAsync(false, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(EmailClient)} failed to send email");
            throw;
        }
    }
}