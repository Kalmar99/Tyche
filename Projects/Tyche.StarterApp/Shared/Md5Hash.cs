using System.Text;

namespace Tyche.StarterApp.Shared;

public static class Md5Hash
{
    public static string Generate(string str)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();

        var inputBytes = Encoding.UTF8.GetBytes(str);
        var outputBytes = md5.ComputeHash(inputBytes);

        return ConvertToString(outputBytes);
    }

    private static string ConvertToString(IEnumerable<byte> bytes)
    {
        var stringBuilder = new StringBuilder();
        
        foreach (var bt in bytes)
        {
            stringBuilder.Append(bt.ToString("X2"));
        }
        
        return stringBuilder.ToString();
    }
}