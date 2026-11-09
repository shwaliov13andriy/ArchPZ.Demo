namespace ArchPZ.Demo.Infrastructure.Services;

using ArchPZ.Demo.Domain.Contracts;
using ArchPZ.Demo.Domain.UserManagement.Repositories;

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
