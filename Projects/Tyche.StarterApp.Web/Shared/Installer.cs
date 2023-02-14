using Tyche.StarterApp.Shared.EmailClient;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Shared;

public static class Installer
{
    public static IServiceCollection AddSharedModule(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfiguration = configuration.GetSection(nameof(EmailSettings));
        
        return services
            .AddEmailClient(emailConfiguration)
            .AddEvents();
    }
}