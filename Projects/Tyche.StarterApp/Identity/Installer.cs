using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public static class Installer
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<IdentitySettings>();
        
        settings.Validate();

        return services
            .AddStorageClient<IdentityStorageSettings>(configuration)
            .AddStorageClient<SaltStorageSettings>(configuration)
            .AddSingleton(settings)
            .AddScoped<SaltRepository>()
            .AddScoped<HashManager>()
            .AddScoped<IdentityRepository>()
            .AddScoped<IdentityStorableEntityFactory>()
            .AddScoped<IIdentityOrchestrator, IdentityOrchestrator>();
    }
}