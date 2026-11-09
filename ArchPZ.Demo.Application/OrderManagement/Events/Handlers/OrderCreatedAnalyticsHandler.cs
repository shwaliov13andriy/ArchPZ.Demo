namespace ArchPZ.Demo.Application.OrderManagement.Events.Handlers;

using Microsoft.Extensions.Logging;
using ArchPZ.Demo.Application.Events;

public class OrderCreatedAnalyticsHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedAnalyticsHandler> _logger;

    public OrderCreatedAnalyticsHandler(ILogger<OrderCreatedAnalyticsHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(OrderCreatedEvent evt, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "Analytics recorded for order {OrderId}. Amount: {TotalAmount}",
            evt.OrderId, evt.TotalAmount);

        return Task.CompletedTask;
    }
}
