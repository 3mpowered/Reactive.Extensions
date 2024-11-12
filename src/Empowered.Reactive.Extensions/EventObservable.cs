using Empowered.Reactive.Extensions.Events;

namespace Empowered.Reactive.Extensions;

internal class EventObservable : IEventObservable
{
    private readonly List<ISubscription> _subscriptions = [];

    public EventObservable(IEnumerable<IEventObserver> observers)
    {
        foreach (var observer in observers)
        {
            Subscribe(observer);
        }
    }

    public IDisposable Subscribe(IObserver<IObservableEvent> observer)
    {
        var subscription = new Subscription(this, observer);
        _subscriptions.Add(subscription);
        return subscription;
    }

    public void Publish(IObservableEvent @event)
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Observer.OnNext(@event);
        }
    }

    public void PublishRange<TEvent>(ICollection<TEvent> events) where TEvent : IObservableEvent
    {
        foreach (var @event in events)
        {
            Publish(@event);
        }
    }

    public void PublishError(Exception exception)
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Observer.OnError(exception);
        }
    }

    public void Unsubscribe(ISubscription subscription)
    {
        _subscriptions.Remove(subscription);
    }
}
