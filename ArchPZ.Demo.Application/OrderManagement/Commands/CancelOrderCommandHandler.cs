namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new InvalidOperationException($"Р—Р°РјРѕРІР»РµРЅРЅСЏ Р· Id '{request.OrderId}' РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        order.Cancel();
        await _orderRepository.UpdateAsync(order, cancellationToken);

        return Unit.Value;
    }
}
