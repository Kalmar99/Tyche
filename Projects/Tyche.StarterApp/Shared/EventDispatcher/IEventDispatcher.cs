namespace Tyche.StarterApp.Shared.EventDispatcher;

public interface IEventDispatcher
{
    public void Dispatch<TEvent>(TEvent @event);
}