using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.CommonUserRepository;

public class CommonUserStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "users";
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(CommonUserStorageSettings)}:{nameof(CommonUserStorageSettings.ConnectionString)} cannot be null or empty");
        }
    }
}