namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Events;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Application.OrderManagement.Events;
using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerValidationService _customerValidationService;
    private readonly IEventBus _bus;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerValidationService customerValidationService,
        IEventBus bus)
    {
        _orderRepository = orderRepository;
        _customerValidationService = customerValidationService;
        _bus = bus;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await _customerValidationService.CustomerExistsAsync(
            request.CustomerId, cancellationToken);

        if (!customerExists)
            throw new InvalidOperationException($"Клієнта з Id '{request.CustomerId}' не знайдено.");

        var address = new Address(request.Street, request.City, request.ZipCode, request.Country);
        var order = Order.Create(request.CustomerId, address, request.Notes);

        foreach (var item in request.Items)
        {
            var unitPrice = new Money(item.UnitPrice, item.Currency);
            order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
        }

        await _orderRepository.AddAsync(order, cancellationToken);

        var total = order.CalculateTotal();
        await _bus.PublishAsync(
            new OrderCreatedEvent(order.Id, request.CustomerId.ToString(), total.Amount),
            cancellationToken);

        return order.ToDto();
    }
}
