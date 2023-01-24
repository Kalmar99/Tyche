namespace Tyche.StarterApp.Shared.StorageClient;

public interface IStorageClient<T>
{
    public Task Set(T entity, CancellationToken ct = default);

    public Task<T> Get(string key,string? partition = null, CancellationToken ct = default);
}