using Microsoft.Extensions.DependencyInjection;

namespace Tyche.StarterApp.Shared.EventDispatcher;

internal class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void Dispatch<TEvent>(TEvent @event)
    {
        using var serviceScope = _serviceProvider.CreateScope();

        var handlers = serviceScope.ServiceProvider.GetService<IEnumerable<IEventHandler<TEvent>>>()?.ToList() ?? new List<IEventHandler<TEvent>>();

        if (!handlers.Any())
        {
            throw new Exception($"Found no handlers for event: {typeof(TEvent).Name}");
        }
        
        Dispatch(@event, handlers);
    }

    private void Dispatch<TEvent>(TEvent @event, IEnumerable<IEventHandler<TEvent>> handlers)
    {
        foreach (var handler in handlers)
        {
            handler.Handle(@event);
        }
    }
}