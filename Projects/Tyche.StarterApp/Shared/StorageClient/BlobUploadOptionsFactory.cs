using Azure.Storage.Blobs.Models;

namespace Tyche.StarterApp.Shared.StorageClient;

internal static class BlobUploadOptionsFactory
{
    public static BlobUploadOptions Create(string? partition)
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