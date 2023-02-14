namespace Tyche.StarterApp.Shared.EmailClient;

public interface IEmailClient
{
    public Task Send(string recipient, string subject, string html, CancellationToken ct = default);
}