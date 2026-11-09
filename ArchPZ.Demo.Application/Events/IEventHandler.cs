namespace ArchPZ.Demo.Application.Events;

public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent evt, CancellationToken ct = default);
}
