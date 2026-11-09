namespace ArchPZ.Demo.Application.OrderManagement.Factories;

using ArchPZ.Demo.Application.OrderManagement.Commands;
using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.ValueObjects;

public interface IOrderFactory
{
    Order CreateNewOrder(CreateOrderCommand command, Address address);
}
