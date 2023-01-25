namespace Tyche.StarterApp.Shared.StorageClient;

public interface IStorageSettings
{
    public string ConnectionString { get; set; }

    public string ContainerName { get; }

    public void Validate();
}