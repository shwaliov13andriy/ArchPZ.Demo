namespace ArchPZ.Demo.Application.OrderManagement.Queries;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

public record GetAllOrdersQuery : IRequest<IReadOnlyList<OrderDto>>;
