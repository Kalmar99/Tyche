namespace Tyche.StarterApp.Shared.StorageClient;

public class StorageEntityMetadata
{
    public StorageEntityMetadata(string key, string partition)
    {
        Key = key;
        Partition = partition;
    }
    
    public string Key { get; }

    public string Partition { get; }
}