using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tyche.StarterApp.Shared.StorageClient;

public static class Installer
{
    public static IServiceCollection AddStorageClient<TSettings>(this IServiceCollection services, IConfiguration configuration) where TSettings : class, IStorageSettings
    {
        var settings = configuration.GetSection(typeof(TSettings).Name).Get<TSettings>();
        
        settings.Validate();

        return services
            .AddSingleton(settings)
            .AddScoped<BlobClientProvider>()
            .AddScoped<IStorageClient<TSettings>, StorageClient<TSettings>>();
    }
}