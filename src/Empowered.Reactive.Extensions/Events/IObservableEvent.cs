namespace Empowered.Reactive.Extensions.Events;

public interface IObservableEvent
{
    public Type Type => GetType();
}
