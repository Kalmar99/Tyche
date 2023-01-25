using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Tyche.StarterApp.Shared.StorageClient;

internal class StorageClient<TSettings> : IStorageClient<TSettings> where TSettings : IStorageSettings
{
    private readonly BlobClientProvider _blobClientProvider;
    private readonly ILogger<StorageClient<TSettings>> _logger;

    public StorageClient(BlobClientProvider blobClientProvider, ILogger<StorageClient<TSettings>> logger)
    {
        _blobClientProvider = blobClientProvider;
        _logger = logger;
    }
    
    public virtual async Task Set<T>(T entity, CancellationToken ct = default) where T : StorageEntity
    {
        try
        {
            var blobClient = await _blobClientProvider.Get(entity.Key, ct);

            var options = BlobUploadOptionsFactory.Create(entity.Partition);

            var blobData = BinaryData.FromString(entity.ToJson());

            await blobClient.UploadAsync(blobData, options, ct);
        }
        catch (Exception e)
        {
            var entityName = nameof(entity);
            var entityKey = entity.Key;
            _logger.LogError(e, "Failed to store entity: {entityName} with key {entityKey}", entityName, entityKey);
            throw;
        }
    }

    public virtual async Task<T> Get<T>(string key, CancellationToken ct = default) where T : StorageEntity
    {
        try
        {
            var blobClient = await _blobClientProvider.Get(key, ct);

            var blob = await blobClient.DownloadAsync(ct);

            await using var blobStream = blob.Value?.Content;

            if (blobStream == null)
            {
                throw new Exception($"null response while downloading blob with key: {key}");
            }

            var json = blobStream.ReadToString();

            if (string.IsNullOrEmpty(json))
            {
                throw new Exception($"failed to parse blob with key: {key} to string");
            }

            return JsonConvert.DeserializeObject<T>(json) ??  throw new Exception($"failed to parse blob with key: {key} to json");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get entity: {key}", key);
            throw;
        }
    }

    public virtual async Task<IReadOnlyCollection<StorageEntityMetadata>> Find(string partition, CancellationToken ct = default)
    {
        try
        {
            var container = _blobClientProvider.Get();

            var results = container.FindByPartitionAsync(partition, ct);

            var blobs = await StorageEntityMetadataFactory.Create(results, partition);

            return blobs;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to find any blobs with partition: {partition}", partition);
            throw;
        }
    }
}