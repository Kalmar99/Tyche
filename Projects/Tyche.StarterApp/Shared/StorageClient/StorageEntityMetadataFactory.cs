using Azure;
using Azure.Storage.Blobs.Models;

namespace Tyche.StarterApp.Shared.StorageClient;

internal static class StorageEntityMetadataFactory
{
    public static async Task<IReadOnlyCollection<StorageEntityMetadata>> Create(AsyncPageable<TaggedBlobItem> blobItems, string partition)
    {
        var blobs = new List<StorageEntityMetadata>();

        await foreach (var blobItem in blobItems)
        {
            blobs.Add(new StorageEntityMetadata(blobItem.BlobName,partition));
        }

        return blobs;
    }
}