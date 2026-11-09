namespace ArchPZ.Demo.Application.OrderManagement.Events;

public record OrderCreatedEvent(Guid OrderId, string CustomerName, decimal TotalAmount);
