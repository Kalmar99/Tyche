namespace Tyche.StarterApp.Shared.StorageClient;

public interface IStorageClient<TSettings>
{
    public Task Set<T>(T entity, CancellationToken ct = default) where T : StorageEntity;

    public Task<T> Get<T>(string key, CancellationToken ct = default) where T : StorageEntity;

    public Task Delete(string key, CancellationToken ct = default);

    public Task<IReadOnlyCollection<StorageEntityMetadata>> Find(string partition, CancellationToken ct = default);
}