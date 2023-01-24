using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace Tyche.StarterApp.Shared.StorageClient;

internal class StorageClient<T> : IStorageClient<T> where T : StorageEntity
{
    private readonly ILogger<StorageClient<T>> _logger;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public StorageClient(StorageSettings settings, ILogger<StorageClient<T>> logger)
    {
        _logger = logger;
        _blobServiceClient = new BlobServiceClient(settings.ConnectionString);
        _containerName = settings.ContainerName;
    }
    
    public virtual async Task Set(T entity, CancellationToken ct = default)
    {
        try
        {
            var blobClient = await CreateBlobClient(_containerName, entity.Key, ct);

            var options = CreateUploadOptions(entity.Partition);

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

    public virtual Task<T> Get(string key, string? partition = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    private async Task<BlobClient> CreateBlobClient(string containerName, string blobKey, CancellationToken ct = default)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
            
        await container.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

        return container.GetBlobClient(blobKey);
    }

    private BlobUploadOptions CreateUploadOptions(string? partition)
    {
        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = "application/json",
                ContentHash = new byte[] //TODO: add
                {
                },
                ContentEncoding = "UTF-8",
                ContentLanguage = null,
                ContentDisposition = null,
                CacheControl = null
            },
            Tags = new Dictionary<string, string>()
        };

        if (partition != null)
        {
            options.Tags.Add("partition", partition);
        }

        return options;
    }
}