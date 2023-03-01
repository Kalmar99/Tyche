using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public class SaltStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "salts";
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(SaltStorageSettings)}:{nameof(SaltStorageSettings.ConnectionString)} cannot be null");
        }
    }
}