using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public class InvitationStorageSettings : IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName => "invitations";
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new ArgumentNullException($"{nameof(InvitationStorageSettings)}:{nameof(ConnectionString)} cannot be null");
        }
    }
}