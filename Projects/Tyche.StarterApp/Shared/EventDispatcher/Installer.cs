using Microsoft.Extensions.DependencyInjection;

namespace Tyche.StarterApp.Shared.EventDispatcher;

public static class Installer
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        return services.AddSingleton<IEventDispatcher, EventDispatcher>();
    }
    
    public static IServiceCollection RegisterEventHandler<TEvent, TImplementation>(this IServiceCollection services) where TImplementation : class, IEventHandler<TEvent>
    {
        return services.AddScoped<IEventHandler<TEvent>, TImplementation>();
    }
}