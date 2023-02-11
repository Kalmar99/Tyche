using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Shared;

public static class Installer
{
    public static IServiceCollection AddSharedModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddEvents();
    }
}