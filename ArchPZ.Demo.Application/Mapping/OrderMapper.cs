namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.OrderManagement.Entities;

public class OrderMapper : IOrderMapper
{
    public OrderDto ToDto(Order order)
    {
        return order.ToDto();
    }

    public OrderItemDto ToDto(OrderItem item)
    {
        return item.ToDto();
    }
}
