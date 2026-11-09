namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;

public record CancelOrderCommand(Guid OrderId) : IRequest<Unit>;
