namespace ArchPZ.Demo.Domain.OrderManagement.Events;

using ArchPZ.Demo.Domain.Common;

public record OrderConfirmedEvent(Guid OrderId, Guid CustomerId, decimal TotalAmount) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
