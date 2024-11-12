using Empowered.Reactive.Extensions.Events;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Empowered.Reactive.Extensions.Tests;

public class EventObservableTests
{
    private readonly IEventObserver _eventObserver = Substitute.For<IEventObserver>();
    private readonly EventObservable _eventObservable;

    public EventObservableTests()
    {
        _eventObservable = new EventObservable([_eventObserver]);
    }

    [Fact]
    public void ShouldReturnSubscriptionOnSubscribe()
    {
        var subscription = _eventObservable.Subscribe(_eventObserver);

        subscription.Should().NotBeNull();
        subscription.Should().BeAssignableTo<ISubscription>();
    }

    [Fact]
    public void ShouldPublishEventToSubscribers()
    {
        var @event = Substitute.For<IObservableEvent>();

        _eventObservable.Publish(@event);

        _eventObserver
            .Received(1)
            .OnNext(@event);
    }

    [Fact]
    public void ShouldPublishRangeEventsToSubscribers()
    {
        var event1 = Substitute.For<IObservableEvent>();
        var event2 = Substitute.For<IObservableEvent>();
        IObservableEvent[] events = [event1, event2];
        _eventObservable.PublishRange(events);

        _eventObserver
            .Received(1)
            .OnNext(event1);
        _eventObserver
            .Received(1)
            .OnNext(event2);
    }

    [Fact]
    public void ShouldPublishExceptionOnError()
    {
        var exception = new InvalidOperationException("error");

        _eventObservable.PublishError(exception);

        _eventObserver
            .Received(1)
            .OnError(exception);
    }

    [Fact]
    public void ShouldNotPublishToDisposedObservers()
    {
        var observable = new EventObservable([]);
        var observer = Substitute.For<IEventObserver>();
        var subscription = observable.Subscribe(observer);

        var event1 = Substitute.For<IObservableEvent>();
        observable.Publish(event1);

        observer
            .Received(1)
            .OnNext(event1);

        subscription.Dispose();

        var event2 = Substitute.For<IObservableEvent>();
        observable.Publish(event2);

        observer
            .DidNotReceive()
            .OnNext(event2);
    }
}
