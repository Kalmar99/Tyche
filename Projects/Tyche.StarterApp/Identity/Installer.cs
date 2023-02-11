using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Identity.Storage;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public static class Installer
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStorageClient<IdentityStorageSettings>(configuration)
            .AddScoped<IdentityRepository>()
            .AddScoped<IdentityStorableEntityFactory>()
            .AddScoped<IIdentityOrchestrator, IdentityOrchestrator>();
    }
}