namespace ArchPZ.Demo.Application.OrderManagement.Events.Handlers;

using ArchPZ.Demo.Application.Contracts;
using ArchPZ.Demo.Application.Events;

public class OrderCreatedNotificationHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly ILoggerService _logger;

    public OrderCreatedNotificationHandler(ILoggerService logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(OrderCreatedEvent evt, CancellationToken ct = default)
    {
        _logger.LogInfo($"Notification sent for order {evt.OrderId}. Customer: {evt.CustomerName}, Total: {evt.TotalAmount}");
        return Task.CompletedTask;
    }
}
