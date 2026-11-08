namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Application.Mapping;
using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

/// <summary>
/// Обробник команди створення замовлення (CQRS: Command Handler).
/// Містить бізнес-логіку use case "Створити замовлення".
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerValidationService _customerValidationService;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerValidationService customerValidationService)
    {
        _orderRepository = orderRepository;
        _customerValidationService = customerValidationService;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Валідація існування клієнта через контракт між Bounded Contexts
        var customerExists = await _customerValidationService.CustomerExistsAsync(
            request.CustomerId, cancellationToken);

        if (!customerExists)
            throw new InvalidOperationException($"Клієнта з Id '{request.CustomerId}' не знайдено.");

        // Створення доменного об'єкта через фабричний метод
        var address = new Address(request.Street, request.City, request.ZipCode, request.Country);
        var order = Order.Create(request.CustomerId, address, request.Notes);

        // Додавання товарів
        foreach (var item in request.Items)
        {
            var unitPrice = new Money(item.UnitPrice, item.Currency);
            order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
        }

        // Збереження через абстракцію репозиторію
        await _orderRepository.AddAsync(order, cancellationToken);

        return order.ToDto();
    }
}
