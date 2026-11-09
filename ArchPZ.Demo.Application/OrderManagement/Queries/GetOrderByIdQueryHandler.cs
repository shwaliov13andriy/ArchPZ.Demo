namespace ArchPZ.Demo.Application.OrderManagement.Queries;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        return order?.ToDto();
    }
}
