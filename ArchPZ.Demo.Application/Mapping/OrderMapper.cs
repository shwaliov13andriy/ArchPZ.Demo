namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.OrderManagement.Entities;

public class OrderMapper : IOrderMapper
{
    public OrderDto ToDto(Order order)
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
            Items = order.Items.Select(ToDto).ToList()
        };
    }

    public OrderItemDto ToDto(OrderItem item)
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
}
