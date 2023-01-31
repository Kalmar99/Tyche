using Tyche.StarterApp.Account;

namespace Tyche.StarterApp;

public static class Installer
{
    public static IServiceCollection AddComponents(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddAccount(configuration);
    }
}