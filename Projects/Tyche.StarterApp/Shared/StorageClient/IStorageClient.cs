namespace Tyche.StarterApp.Shared.StorageClient;

public interface IStorageClient<TSettings>
{
    public Task Set<T>(T entity, CancellationToken ct = default) where T : StorageEntity;

    public Task<T> Get<T>(string key, CancellationToken ct = default) where T : StorageEntity;
}