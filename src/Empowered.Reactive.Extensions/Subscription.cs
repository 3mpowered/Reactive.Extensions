using Empowered.Reactive.Extensions.Events;

namespace Empowered.Reactive.Extensions;

internal class Subscription(
    IEventObservable observable,
    IObserver<IObservableEvent> observer
) : ISubscription
{
    private IEventObservable? _observable = observable;
    public IObserver<IObservableEvent> Observer => observer;

    public void Dispose()
    {
        observer.OnCompleted();
        _observable?.Unsubscribe(this);
        _observable = null;
    }
}
