using Empowered.Reactive.Extensions.Events;

namespace Empowered.Reactive.Extensions;

public interface IEventObservable : IObservable<IObservableEvent>
{
    void Publish(IObservableEvent @event);
    void PublishRange<TEvent>(ICollection<TEvent> events) where TEvent : IObservableEvent;
    void PublishError(Exception exception);
    void Unsubscribe(ISubscription subscription);
}
