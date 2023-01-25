using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Tyche.StarterApp.Shared.StorageClient;

internal class BlobClientProvider<TSettings> where TSettings : class, IStorageSettings
{
    private readonly BlobContainerClient _blobClient;

    public BlobClientProvider(TSettings settings)
    {
        _blobClient = new BlobServiceClient(settings.ConnectionString).GetBlobContainerClient(settings.ContainerName);
    }

    public async Task<BlobClient> Get(string blobKey, CancellationToken ct = default)
    {
        await _blobClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: ct);

        return _blobClient.GetBlobClient(blobKey);
    }

    public BlobContainerClient Get() => _blobClient;
}