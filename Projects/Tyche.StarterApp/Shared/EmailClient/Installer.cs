using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tyche.StarterApp.Shared.EmailClient;

public static class Installer
{
    public static IServiceCollection AddEmailClient(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<EmailSettings>();
        
        settings.Validate();

        return services
            .AddSingleton(settings)
            .AddScoped<IEmailClient, EmailClient>();
    }
}