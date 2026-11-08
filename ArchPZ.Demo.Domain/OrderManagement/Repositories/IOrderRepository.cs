namespace ArchPZ.Demo.Domain.OrderManagement.Repositories;

using ArchPZ.Demo.Domain.OrderManagement.Entities;

/// <summary>
/// Інтерфейс репозиторію замовлень.
/// Визначається в Domain, реалізується в Infrastructure.
/// </summary>
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
