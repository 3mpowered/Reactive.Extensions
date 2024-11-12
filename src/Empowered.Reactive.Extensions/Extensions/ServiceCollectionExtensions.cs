using Microsoft.Extensions.DependencyInjection;

namespace Empowered.Reactive.Extensions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventObservable(this IServiceCollection collection) =>
        collection.AddScoped<IEventObservable, EventObservable>();
}
