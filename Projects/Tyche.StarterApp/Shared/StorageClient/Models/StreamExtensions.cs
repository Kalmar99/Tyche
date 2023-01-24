namespace Tyche.StarterApp.Shared.StorageClient;

public static class StreamExtensions
{
    public static string ReadToString(this Stream stream)
    {
        using var reader = new StreamReader(stream);

        var json = reader.ReadToEnd();

        return json;
    }
}