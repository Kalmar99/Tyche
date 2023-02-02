using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Account;

public static class Installer
{
    public static IServiceCollection AddAccountComponent(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStorageClient<AccountStorageSettings>(configuration)
            .AddStorageClient<UserStorageSettings>(configuration)
            .AddScoped<UserFactory>()
            .AddScoped<AccountFactory>()
            .AddScoped<AccountRepository>()
            .AddScoped<UserRepository>()
            .AddScoped<AccountService>()
            .AddScoped<IAccountOrchestrator, AccountOrchestrator>();
    }
}