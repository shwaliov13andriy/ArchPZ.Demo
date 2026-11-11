namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;

public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderMapper _orderMapper;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository, IOrderMapper orderMapper)
    {
        _orderRepository = orderRepository;
        _orderMapper = orderMapper;
    }

    public async Task<OrderDto> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new InvalidOperationException($"Замовлення з Id '{request.OrderId}' не знайдено.");

        order.Confirm();

        await _orderRepository.UpdateAsync(order, cancellationToken);

        return _orderMapper.ToDto(order);
    }
}
