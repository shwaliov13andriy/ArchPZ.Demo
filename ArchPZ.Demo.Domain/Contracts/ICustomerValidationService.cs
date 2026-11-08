namespace ArchPZ.Demo.Domain.Contracts;

/// <summary>
/// Контракт взаємодії між Bounded Contexts.
/// OrderManagement використовує цей інтерфейс для валідації існування клієнта,
/// а UserManagement надає реалізацію.
/// </summary>
public interface ICustomerValidationService
{
    Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<string?> GetCustomerEmailAsync(Guid customerId, CancellationToken cancellationToken = default);
}
