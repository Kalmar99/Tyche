using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EventDispatcher;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

public static class Installer
{
    public static IServiceCollection AddAccountModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStorageClient<AccountStorageSettings>(configuration)
            .AddStorageClient<UserStorageSettings>(configuration)
            .AddScoped<AccountRepository>()
            .AddScoped<UserRepository>()
            .AddScoped<AccountService>()
            .RegisterEventHandler<IdentityRegisteredEvent, AccountEventHandler>()
            .AddScoped<IAccountOrchestrator, AccountOrchestrator>();
    }
}