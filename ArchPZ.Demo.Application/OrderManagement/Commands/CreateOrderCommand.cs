namespace ArchPZ.Demo.Application.OrderManagement.Commands;

using MediatR;
using ArchPZ.Demo.Application.DTOs;

/// <summary>
/// Команда створення нового замовлення (CQRS: Command)
/// </summary>
public record CreateOrderCommand : IRequest<OrderDto>
{
    public Guid CustomerId { get; init; }
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public List<CreateOrderItemCommand> Items { get; init; } = new();
}

public record CreateOrderItemCommand
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public string Currency { get; init; } = "UAH";
}
