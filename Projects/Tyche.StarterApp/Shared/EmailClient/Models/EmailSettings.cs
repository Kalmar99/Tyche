namespace Tyche.StarterApp.Shared.EmailClient;

public class EmailSettings
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string SmtpServer { get; set; }

    public int SmtpPort { get; set; }

    public string SenderEmail { get; set; }

    public bool IsDevelopment { get; set; }

    public void Validate()
    {
        if (Username == null)
        {
            throw new ArgumentNullException($"{nameof(EmailSettings)}:{Username} cannot be null or empty");
        }
        
        if (Password == null)
        {
            throw new ArgumentNullException($"{nameof(EmailSettings)}:{Password} cannot be null or empty");
        }
        
        if (string.IsNullOrEmpty(SmtpServer))
        {
            throw new ArgumentNullException($"{nameof(EmailSettings)}:{SmtpServer} cannot be null or empty");
        }
        
        if (SmtpPort.Equals(0) || SmtpPort == null)
        {
            throw new ArgumentNullException($"{nameof(EmailSettings)}:{SmtpPort} is not valid");
        }
        
        if (string.IsNullOrEmpty(SenderEmail))
        {
            throw new ArgumentNullException($"{nameof(EmailSettings)}:{SenderEmail} cannot be null or empty");
        }
    }
}