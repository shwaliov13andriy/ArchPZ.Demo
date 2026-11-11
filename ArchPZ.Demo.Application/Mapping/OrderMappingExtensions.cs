namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.OrderManagement.Entities;

public static class OrderMappingExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Status = order.Status.ToString(),
            ShippingAddress = order.ShippingAddress.ToString(),
            TotalAmount = order.CalculateTotal().Amount,
            Notes = order.Notes,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(i => i.ToDto()).ToList()
        };
    }

    public static OrderItemDto ToDto(this OrderItem item)
    {
        return new OrderItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice.Amount,
            TotalPrice = item.GetTotalPrice().Amount
        };
    }

    public static IReadOnlyList<OrderDto> ToDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(ToDto).ToList();
    }
}
