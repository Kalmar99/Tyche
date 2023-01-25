namespace Tyche.StarterApp.Account;

public static class Installer
{
    public static IServiceCollection AddAccount(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddAccountComponent(configuration);
    }
}