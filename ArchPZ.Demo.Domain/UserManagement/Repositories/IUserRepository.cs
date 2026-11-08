namespace ArchPZ.Demo.Domain.UserManagement.Repositories;

using ArchPZ.Demo.Domain.UserManagement.Entities;

/// <summary>
/// Інтерфейс репозиторію користувачів.
/// Визначається в Domain, реалізується в Infrastructure.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}
