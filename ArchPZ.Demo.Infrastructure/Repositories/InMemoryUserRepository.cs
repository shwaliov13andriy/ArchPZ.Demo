namespace ArchPZ.Demo.Infrastructure.Repositories;

using ArchPZ.Demo.Domain.UserManagement.Entities;
using ArchPZ.Demo.Domain.UserManagement.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private readonly object _lock = new();

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Email.Value == email.ToLowerInvariant());
            return Task.FromResult(user);
        }
    }

    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            IReadOnlyList<User> result = _users.ToList();
            return Task.FromResult(result);
        }
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            _users.Add(user);
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index >= 0)
                _users[index] = user;
        }
        return Task.CompletedTask;
    }
}
