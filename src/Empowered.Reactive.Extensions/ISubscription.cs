using Empowered.Reactive.Extensions.Events;

namespace Empowered.Reactive.Extensions;

public interface ISubscription : IDisposable
{
    IObserver<IObservableEvent> Observer { get; }
}
