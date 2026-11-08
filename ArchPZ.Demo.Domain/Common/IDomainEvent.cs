namespace ArchPZ.Demo.Domain.Common;

/// <summary>
/// Маркерний інтерфейс для доменних подій
/// </summary>
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
