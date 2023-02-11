namespace Tyche.StarterApp.Shared.EventDispatcher;

public interface IEventHandler<in TEvent>
{
    public Task Handle(TEvent @event, CancellationToken ct = default);
}