using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Shared.CommonUserRepository;

public static class Installer
{
    public static IServiceCollection AddCommonUserRepository(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddStorageClient<CommonUserStorageSettings>(configuration)
            .AddScoped<ICommonUserRepository, CommonUserRepository>();
    }
}