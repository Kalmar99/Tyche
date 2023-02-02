using Tyche.StarterApp.Shared.HashManager;

namespace Tyche.StarterApp.Shared;

public static class Installer
{
    public static IServiceCollection AddSharedModule(this IServiceCollection services, IConfiguration configuration)
    {
        var hashManagerConfig = configuration.AddInMemoryVariable("SaltStorageAccount", $"{nameof(SaltStorageSettings)}:{nameof(SaltStorageSettings.ConnectionString)}");

        return services.AddHashManager(hashManagerConfig);
    }
}