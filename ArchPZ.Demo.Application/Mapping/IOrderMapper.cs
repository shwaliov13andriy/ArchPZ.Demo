namespace ArchPZ.Demo.Application.Mapping;

using ArchPZ.Demo.Application.DTOs;
using ArchPZ.Demo.Domain.OrderManagement.Entities;

public interface IOrderMapper
{
    OrderDto ToDto(Order order);
    OrderItemDto ToDto(OrderItem item);
}
