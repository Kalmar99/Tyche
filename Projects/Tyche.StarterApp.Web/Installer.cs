using Tyche.StarterApp.Account;
using Tyche.StarterApp.Identity;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp;

public static class Installer
{
    public static IServiceCollection AddComponents(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSharedModule(configuration)
            .AddAccount(configuration)
            .AddIdentity(configuration);
    }
}