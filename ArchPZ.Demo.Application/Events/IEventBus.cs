namespace ArchPZ.Demo.Application.Events;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent evt, CancellationToken ct = default);
}
