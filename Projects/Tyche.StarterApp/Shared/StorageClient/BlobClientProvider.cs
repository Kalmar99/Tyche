using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace Tyche.StarterApp.Shared.StorageClient;

internal class BlobClientProvider<TSettings> where TSettings : class, IStorageSettings
{
    private readonly ILogger<BlobClientProvider<TSettings>> _logger;
    private readonly BlobContainerClient _blobClient;

    public BlobClientProvider(TSettings settings, ILogger<BlobClientProvider<TSettings>> logger)
    {
        _logger = logger;
        _blobClient = new BlobServiceClient(settings.ConnectionString).GetBlobContainerClient(settings.ContainerName);
    }

    public async Task<BlobClient> Get(string blobKey, CancellationToken ct = default)
    {
        try
        {
            await _blobClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

            return _blobClient.GetBlobClient(blobKey);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get blob client for key: {blobKey}", blobKey);
            throw;
        }
    }

    public BlobContainerClient Get() => _blobClient;
}