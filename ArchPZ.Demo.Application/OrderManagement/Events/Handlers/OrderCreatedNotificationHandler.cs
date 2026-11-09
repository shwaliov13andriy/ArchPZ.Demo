namespace ArchPZ.Demo.Application.OrderManagement.Events.Handlers;

using Microsoft.Extensions.Logging;
using ArchPZ.Demo.Application.Events;

public class OrderCreatedNotificationHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedNotificationHandler> _logger;

    public OrderCreatedNotificationHandler(ILogger<OrderCreatedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(OrderCreatedEvent evt, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "Notification sent for order {OrderId}. Customer: {CustomerName}, Total: {TotalAmount}",
            evt.OrderId, evt.CustomerName, evt.TotalAmount);

        return Task.CompletedTask;
    }
}
