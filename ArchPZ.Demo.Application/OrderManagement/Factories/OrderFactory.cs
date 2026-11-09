namespace ArchPZ.Demo.Application.OrderManagement.Factories;

using ArchPZ.Demo.Application.OrderManagement.Commands;
using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class OrderFactory : IOrderFactory
{
    public Order CreateNewOrder(CreateOrderCommand command, Address address)
    {
        var order = Order.Create(command.CustomerId, address, command.Notes);

        foreach (var item in command.Items)
        {
            var unitPrice = new Money(item.UnitPrice, item.Currency);
            order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
        }

        return order;
    }
}
