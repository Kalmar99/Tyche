using System.Security.Cryptography;
using System.Text;
using Azure.Storage.Blobs.Models;

namespace Tyche.StarterApp.Shared.StorageClient;

internal static class BlobUploadOptionsFactory
{
    public static BlobUploadOptions Create(string? partition, byte[] content)
    {
        using var md5 = MD5.Create();

        var hash = md5.ComputeHash(content);

        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = "application/json",
                ContentHash = hash,
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