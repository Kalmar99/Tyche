using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Shared.SecureHasher;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.HashManager;

public static class Installer
{
    public static IServiceCollection AddHashManager(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStorageClient<SaltStorageSettings>(configuration)
            .AddScoped<SaltRepository>()
            .AddScoped<IHashManager, HashManager>();

    }
}