using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity.Storage;

public class IdentityStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "identity";
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(IdentityStorageSettings)}:{nameof(ConnectionString)} cannot be null or empty");
        }
    }
}