namespace ArchPZ.Demo.Application.OrderManagement.Queries;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

/// <summary>
/// Запит отримання замовлення за Id (CQRS: Query)
/// </summary>
public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto?>;
