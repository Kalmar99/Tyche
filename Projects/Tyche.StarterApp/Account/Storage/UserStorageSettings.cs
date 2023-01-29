using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

public class UserStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "users";

    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(UserStorageSettings)}:{nameof(ConnectionString)} cannot be null or empty");
        }
        
        if (string.IsNullOrEmpty(ContainerName))
        {
            throw new ArgumentNullException($"{nameof(UserStorageSettings)}:{nameof(ContainerName)} cannot be null or empty");
        }
    }
}