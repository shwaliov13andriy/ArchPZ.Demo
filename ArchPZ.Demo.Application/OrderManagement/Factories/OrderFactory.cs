namespace ArchPZ.Demo.Application.OrderManagement.Factories;

using ArchPZ.Demo.Application.OrderManagement.Commands;
using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public class OrderFactory : IOrderFactory
{
    public Order CreateNewOrder(CreateOrderCommand command, Address address)
    {
        var order = Order.Create(command.CustomerId, address, command.Notes);

        if (command.Items.Count == 0 && command.TotalAmount > 0)
        {
            order.AddItem(Guid.NewGuid(), "Товар", 1, new Money(command.TotalAmount, "UAH"));
        }
        else
        {
            foreach (var item in command.Items)
            {
                var unitPrice = new Money(item.UnitPrice, item.Currency);
                order.AddItem(item.ProductId, item.ProductName, item.Quantity, unitPrice);
            }
        }

        return order;
    }
}
