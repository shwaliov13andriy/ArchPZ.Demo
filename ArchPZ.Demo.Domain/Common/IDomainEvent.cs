namespace ArchPZ.Demo.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
