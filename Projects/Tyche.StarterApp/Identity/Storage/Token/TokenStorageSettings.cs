using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Token;

public class TokenStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "tokens";
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(TokenStorageSettings)}:{nameof(ConnectionString)} cannot be null");
        }
    }
}