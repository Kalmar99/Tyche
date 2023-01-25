using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Tyche.StarterApp.Shared.StorageClient;

internal static class BlobContainerClientExtensions
{
    public static AsyncPageable<TaggedBlobItem> FindByPartitionAsync(this BlobContainerClient client, string partition, CancellationToken ct = default)
    {
        var query = $"partition={partition}";

        return client.FindBlobsByTagsAsync(query, ct);
    }
}