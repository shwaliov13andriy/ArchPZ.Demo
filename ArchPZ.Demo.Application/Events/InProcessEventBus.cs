namespace ArchPZ.Demo.Application.Events;

using Microsoft.Extensions.DependencyInjection;

public class InProcessEventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;

    public InProcessEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent evt, CancellationToken ct = default)
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(evt, ct);
        }
    }
}
