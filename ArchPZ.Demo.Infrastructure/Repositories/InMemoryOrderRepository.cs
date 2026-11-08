namespace ArchPZ.Demo.Infrastructure.Repositories;

using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.OrderManagement.Repositories;

/// <summary>
/// In-Memory реалізація репозиторію замовлень.
/// У реальному проєкті тут була б робота з EF Core / Dapper / іншою ORM.
/// Реалізація визначена в Infrastructure, інтерфейс — у Domain.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    private readonly object _lock = new();

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(order);
        }
    }

    public Task<IReadOnlyList<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            IReadOnlyList<Order> result = _orders.Where(o => o.CustomerId == customerId).ToList();
            return Task.FromResult(result);
        }
    }

    public Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            IReadOnlyList<Order> result = _orders.ToList();
            return Task.FromResult(result);
        }
    }

    public Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            _orders.Add(order);
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var index = _orders.FindIndex(o => o.Id == order.Id);
            if (index >= 0)
                _orders[index] = order;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            _orders.RemoveAll(o => o.Id == id);
        }
        return Task.CompletedTask;
    }
}
