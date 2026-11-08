namespace ArchPZ.Demo.Domain.OrderManagement.Events;

using ArchPZ.Demo.Domain.Common;

public record OrderCreatedEvent(Guid OrderId, Guid CustomerId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
