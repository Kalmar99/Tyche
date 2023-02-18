using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Identity.Token;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Identity;

public static class Installer
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
        
        settings.Validate();

        return services
            .AddStorageClient<IdentityStorageSettings>(configuration)
            .AddStorageClient<SaltStorageSettings>(configuration)
            .AddStorageClient<InvitationStorageSettings>(configuration)
            .RegisterEventHandler<UserInvitedEvent, IdentityEventHandler>()
            .AddSingleton(settings)
            .AddScoped<SaltRepository>()
            .AddScoped<InvitationRepository>()
            .AddScoped<HashManager>()
            .AddScoped<IdentityRepository>()
            .AddScoped<IdentityStorableEntityFactory>()
            .AddScoped<IIdentityOrchestrator, IdentityOrchestrator>();
    }
}