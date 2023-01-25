using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

public class AccountStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "accounts";

    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(AccountStorageSettings)}:{nameof(ConnectionString)} cannot be null or empty");
        }
        
        if (string.IsNullOrEmpty(ContainerName))
        {
            throw new ArgumentNullException($"{nameof(AccountStorageSettings)}:{nameof(ContainerName)} cannot be null or empty");
        }
    }
}