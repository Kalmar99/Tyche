namespace Tyche.StarterApp.Shared;

public static class ConfigurationExtensions
{
    public static IConfiguration AddInMemoryVariable(this IConfiguration configuration, string reference, string key)
    {
        var value = configuration.GetValue<string>(reference);

        return new ConfigurationBuilder().AddInMemoryCollection(new List<KeyValuePair<string, string>>(1)
        {
            new KeyValuePair<string, string>(key, value)
        }).AddConfiguration(configuration).Build();
    }
}