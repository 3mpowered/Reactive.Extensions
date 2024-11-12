using Empowered.Reactive.Extensions.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Empowered.Reactive.Extensions.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void ShouldAddEventObservableToCollection()
    {
        var collection = new ServiceCollection()
            .AddEventObservable();

        var serviceProvider = collection.BuildServiceProvider();

        var observable = serviceProvider.GetRequiredService<IEventObservable>();

        observable.Should().NotBeNull();
    }
}
