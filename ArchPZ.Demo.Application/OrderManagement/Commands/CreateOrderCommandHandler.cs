namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Events;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Application.OrderManagement.Events;
using ArchPZ.Demo.Application.OrderManagement.Factories;
using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerValidationService _customerValidationService;
    private readonly IEventBus _bus;
    private readonly IOrderFactory _orderFactory;
    private readonly IOrderMapper _orderMapper;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerValidationService customerValidationService,
        IEventBus bus,
        IOrderFactory orderFactory,
        IOrderMapper orderMapper)
    {
        _orderRepository = orderRepository;
        _customerValidationService = customerValidationService;
        _bus = bus;
        _orderFactory = orderFactory;
        _orderMapper = orderMapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await _customerValidationService.CustomerExistsAsync(
            request.CustomerId, cancellationToken);

        if (!customerExists)
            throw new InvalidOperationException($"Клієнта з Id '{request.CustomerId}' не знайдено.");

        var address = new Address(request.Street, request.City, request.ZipCode, request.Country);
        var order = _orderFactory.CreateNewOrder(request, address);

        await _orderRepository.AddAsync(order, cancellationToken);

        var total = order.CalculateTotal();
        await _bus.PublishAsync(
            new OrderCreatedEvent(order.Id, request.CustomerId.ToString(), total.Amount),
            cancellationToken);

        return _orderMapper.ToDto(order);
    }
}
