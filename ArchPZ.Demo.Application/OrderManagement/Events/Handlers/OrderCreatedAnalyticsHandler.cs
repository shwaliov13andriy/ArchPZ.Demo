namespace ArchPZ.Demo.Application.OrderManagement.Events.Handlers;

using ArchPZ.Demo.Application.Contracts;
using ArchPZ.Demo.Application.Events;

public class OrderCreatedAnalyticsHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly ILoggerService _logger;

    public OrderCreatedAnalyticsHandler(ILoggerService logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(OrderCreatedEvent evt, CancellationToken ct = default)
    {
        _logger.LogInfo($"Analytics recorded for order {evt.OrderId}. Amount: {evt.TotalAmount}");
        return Task.CompletedTask;
    }
}
