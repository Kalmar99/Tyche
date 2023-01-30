using Newtonsoft.Json;

namespace Tyche.StarterApp.Shared.StorageClient;

public abstract class StorageEntity
{
    public StorageEntity(string key, string? partition = null)
    {
        Key = key;
        Partition = partition;
    }

    public string Key { get; init; }

    public string? Partition { get; }

    public string ToJson() => JsonConvert.SerializeObject(this);
}