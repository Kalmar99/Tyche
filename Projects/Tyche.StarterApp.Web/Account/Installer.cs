using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Account;

public static class Installer
{
    public static IServiceCollection AddAccount(this IServiceCollection services, IConfiguration configuration)
    {
        var componentConfiguration = configuration
            .AddInMemoryVariable("StorageAccount", $"{nameof(AccountStorageSettings)}:{nameof(AccountStorageSettings.ConnectionString)}")
            .AddInMemoryVariable("StorageAccount", $"{nameof(UserStorageSettings)}:{nameof(UserStorageSettings.ConnectionString)}");

        return services.AddAccountModule(componentConfiguration);
    }
}