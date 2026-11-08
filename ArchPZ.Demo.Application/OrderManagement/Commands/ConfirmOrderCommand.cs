namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

/// <summary>
/// Команда підтвердження замовлення
/// </summary>
public record ConfirmOrderCommand(Guid OrderId) : IRequest<OrderDto>;
