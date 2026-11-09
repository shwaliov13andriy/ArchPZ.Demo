namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

public record ConfirmOrderCommand(Guid OrderId) : IRequest<OrderDto>;
