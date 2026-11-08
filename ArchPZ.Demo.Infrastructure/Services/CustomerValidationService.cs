namespace ArchPZ.Demo.Infrastructure.Services;

using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.UserManagement.Repositories;

/// <summary>
/// Реалізація контракту взаємодії між Bounded Contexts.
/// 
/// OrderManagement потребує перевірки існування клієнта, але не повинен
/// напряму залежати від UserManagement. Тому:
/// - Інтерфейс ICustomerValidationService визначено у Domain/Contracts
/// - Реалізація знаходиться в Infrastructure і використовує IUserRepository
/// 
/// Це забезпечує слабку зв'язність (loose coupling) між контекстами.
/// </summary>
public class CustomerValidationService : ICustomerValidationService
{
    private readonly IUserRepository _userRepository;

    public CustomerValidationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(customerId, cancellationToken);
        return user != null && user.IsActive;
    }

    public async Task<string?> GetCustomerEmailAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(customerId, cancellationToken);
        return user?.Email.Value;
    }
}
